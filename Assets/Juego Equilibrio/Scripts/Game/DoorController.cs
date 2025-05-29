using UnityEngine;
public class DoorController : InteractiveObject
{
    //[SerializeField] private AudioClipSO audioDoor;

    [Header("Rotation")]
    [SerializeField] private Vector3 rotationOpen;
    public Vector3 RotationOpen => rotationOpen;
    private Quaternion open;
    private Quaternion close;

    private void Start()
    {
        open = Quaternion.Euler(rotationOpen);
        close = transform.rotation;
    }
    private void Update()
    {
        if (input)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, open, 5 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, close, 5 * Time.deltaTime);
        }
    }
    protected override void Interactive()
    {
        if (!input)
        {
            input = true;
        }
        else
        {
            input = false;
        }
    }
}