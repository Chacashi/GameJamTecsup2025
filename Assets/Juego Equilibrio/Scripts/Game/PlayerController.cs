using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraTransform;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private Vector2 directionCamera;
    private Vector2 direction;
    private Rigidbody _compRigibody;


    private void Awake()
    {
        _compRigibody= GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        InputReader.OnPlayerMovement += SetDirection;
        InputReader.OnPlayerMovement += SetDirectionCamera;
    }

    private void OnDisable()
    {
        InputReader.OnPlayerMovement -= SetDirection;
        InputReader.OnPlayerMovement -= SetDirectionCamera;
    }

    void SetDirection( Vector2 newDirection)
    {
        direction = newDirection;
    }

    private void SetDirectionCamera(Vector2 newDirection)
    {
        directionCamera = newDirection;
    }

    private void FixedUpdate()
    {
        cameraForward = cameraTransform.forward;
        cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        _compRigibody.linearVelocity = new Vector3(speed * direction.x, _compRigibody.linearVelocity.y, speed * direction.y);
        direction = cameraRight * directionCamera.x + cameraForward * directionCamera.y;
    }
}
