using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource playerAudio;

    [SerializeField] private float speed;
    [SerializeField] private Transform cameraTransform;

    private Vector2 inputDirection;
    private Rigidbody _rigidbody;
    [Header("Event Sound")]
    [SerializeField] private float distanceSound;
    public static event Action<Vector3, float> OnEventSound;
    [Header("UIManager")]
    [SerializeField] private UIManager manager;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    private void Reset()
    {
        distanceSound = 100;
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        float normalizedValue = manager.BarTime.value / manager.BarTime.maxValue;
        float invertedValue = 1f - normalizedValue;
        float velocidadFactor = 1f - Mathf.Pow(invertedValue * 2f - 1f, 2f); 
        speed = Mathf.Lerp(speedMin, speedMax, velocidadFactor);
        HandleMovementSound();
    }
    private void OnEnable()
    {
        InputReader.OnPlayerMovement += SetDirection;
    }

    private void OnDisable()
    {
        InputReader.OnPlayerMovement -= SetDirection;
    }
    private void HandleMovementSound()
    {
        if (inputDirection != Vector2.zero)
        {
            if (!playerAudio.isPlaying)
            {
                playerAudio.Play();
            }
        }
        else
        {
            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
        }
    }
    
    private void SetDirection(Vector2 newDirection)
    {
        if (newDirection != Vector2.zero)
        {
            OnEventSound?.Invoke(transform.position, distanceSound);
        }
        inputDirection = newDirection;
    }

    private void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camRight * inputDirection.x + camForward * inputDirection.y;

        _rigidbody.linearVelocity = new Vector3(
            moveDirection.x * speed,
            _rigidbody.linearVelocity.y,
            moveDirection.z * speed
        );
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.instance.Fail();
            Debug.Log("Entro");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Entro");
        }
    }
}

