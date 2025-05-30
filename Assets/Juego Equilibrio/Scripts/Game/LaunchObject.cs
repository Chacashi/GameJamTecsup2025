using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class LaunchObject : Item
{
    [SerializeField] private float launchModifier;
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private int trajectoryPoints = 30;
    [SerializeField] private float trajectoryStep = 0.1f;
    private bool shoot;

    private LineRenderer lineRenderer;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (input)
        {
            if (shoot)
            {
                launchModifier += Time.deltaTime * 2;
                launchModifier = Mathf.Clamp(launchModifier, minDistance, maxDistance);

                Vector3 startPosition = transform.position;
                Vector3 initialVelocity = cameraMain.forward.normalized * launchModifier;
                DrawTrajectory(startPosition, initialVelocity);
                lineRenderer.enabled = true;
            }
            else
            {
                if (launchModifier > minDistance)
                {
                    rb.isKinematic = false;
                    rb.linearVelocity = cameraMain.forward.normalized * launchModifier;
                    launchModifier = 0;
                    transform.SetParent(null);
                    collider.enabled = true;
                }
                lineRenderer.enabled = false;
            }
        }
    }

    public void Shoot(bool value)
    {
        shoot = value;
    }

    private void OnEnable()
    {
        InputReader.shoot2 += Shoot;
    }

    private void OnDisable()
    {
        InputReader.shoot2 -= Shoot;
    }

    private void DrawTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        lineRenderer.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * trajectoryStep;

            Vector3 position = startPosition + initialVelocity * time + 0.5f * Physics.gravity * time * time;

            lineRenderer.SetPosition(i, position);
        }
    }
}
