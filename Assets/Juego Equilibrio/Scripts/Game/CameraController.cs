using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 movementCamera;
    [SerializeField] private float velocity;
    private CinemachineCamera camPlayer;
    [SerializeField] private Transform camPrincipal;

    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

    [SerializeField] LayerMask objectLayer;
    private RaycastHit raycastObject;
    private bool confirmsInput;

    private void Awake()
    {
        camPlayer = GetComponent<CinemachineCamera>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void MovementCamera(Vector2 value)
    {
        movementCamera.x += value.x * velocity * Time.deltaTime;
        movementCamera.y += value.y * velocity * Time.deltaTime;

        movementCamera.y = Mathf.Clamp(movementCamera.y, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(-movementCamera.y, movementCamera.x, 0f);
    }

    private void OnEnable()
    {
        InputReader.OnMovementCamera += MovementCamera;
    }

    private void OnDisable()
    {
        InputReader.OnMovementCamera -= MovementCamera;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(camPrincipal.transform.position, camPrincipal.forward * 2f, Color.green);
        if (Physics.Raycast(camPrincipal.transform.position, camPrincipal.transform.forward, out raycastObject, 2f, objectLayer))
        {
            if (confirmsInput)
            {
              
            }
        }
        else
        {
            if (!confirmsInput)
            {
               
            }
        }
    }
}
