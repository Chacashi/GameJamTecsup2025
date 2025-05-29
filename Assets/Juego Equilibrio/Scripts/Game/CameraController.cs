using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 movementCamera;
    [SerializeField] private float velocity;

    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

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
}
