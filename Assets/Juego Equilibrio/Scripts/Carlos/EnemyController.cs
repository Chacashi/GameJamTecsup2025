using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    private NavMeshAgent agent;
    private StateEnemy stateEnemy;
    private Animator animator;

    [Header("Point Patrol")]
    [SerializeField] private Transform[] points;
    private int currentPointIndex;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolWaitTime;
    [SerializeField] private float arriveDistance;
    private bool isRotating = false;
    private Quaternion targetRotation;

    [Header("Characteristic")]
    [SerializeField] private float radioRamdom;
    private float patrolTimer = 0f;
    [SerializeField] private float listenRange;

    private Vector3 center2;

    [Header("Speed Settings")]
    [SerializeField] private float baseSpeed = 3.5f;
    [SerializeField] private float speedIncreasePerSound = 0.5f;
    [SerializeField] private float maxSpeed = 7f;

    private void Reset()
    {
        patrolWaitTime = 2f;
        arriveDistance = 0.5f;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stateEnemy = StateEnemy.Patrol;
        agent.speed = baseSpeed;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (stateEnemy)
        {
            case StateEnemy.Patrol:
                Patrol();
                CheckDoorInteraction();
                break;
            case StateEnemy.Atack:
                CheckDoorInteraction();
                break;
            case StateEnemy.IrDondeLabulla:
                CheckDoorInteraction();
                break;
        }
    }

    private void Patrol()
    {
        if (points.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= arriveDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                patrolTimer = 0f;
                Vector3 nextPoint = GetRandomPoint(transform.position, radioRamdom);
                StartCoroutine(RotateThenMove(nextPoint));
            }
        }
    }

    private IEnumerator RotateThenMove(Vector3 destination)
    {
        isRotating = true;
        agent.isStopped = true;

        Vector3 direction = (destination - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;

        isRotating = false;
        agent.isStopped = false;
        Destination(destination);
    }

    private void GoToNextPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % points.Length;
        Destination(points[currentPointIndex].position);
    }

    private void Destination(Vector3 destination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 10, NavMesh.AllAreas))
        {
            agent.SetDestination(new Vector3(destination.x, transform.position.y, destination.z));
        }
        else
        {
            Debug.Log("No se encontró una posición válida en el NavMesh.");
        }
    }

    private Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            randomPos.y = transform.position.y;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 2.0f, NavMesh.AllAreas))
            {
                center2 = hit.position;
                return hit.position;
            }
        }
        return center;
    }

    private void Ir(Vector3 position)
    {
        stateEnemy = StateEnemy.IrDondeLabulla;
        Destination(position);
    }

    private void GetSoundPosition(Vector3 position, float distance)
    {
        if (Vector3.Distance(transform.position, position) < distance)
        {
            agent.speed = Mathf.Min(agent.speed + speedIncreasePerSound, maxSpeed);
            Ir(position);
        }
    }

    private void CheckDoorInteraction()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        RaycastHit hit;
        float checkDistance = 2f;

        if (Physics.Raycast(ray, out hit, checkDistance))
        {
            DoorController door = hit.collider.GetComponent<DoorController>();
            if (door != null)
            {
                Debug.Log("Puerta detectada por el enemigo. Abriendo puerta...");
                door.OpenDoor();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(center2, 0.5f);
        Gizmos.DrawWireSphere(transform.position, radioRamdom);
        Gizmos.DrawLine(transform.position, center2);
    }

    private void OnEnable()
    {
        Item.OnEventSound += GetSoundPosition;
        PlayerController.OnEventSound += GetSoundPosition;
    }

    private void OnDisable()
    {
        Item.OnEventSound -= GetSoundPosition;
        PlayerController.OnEventSound -= GetSoundPosition;
    }

    private enum StateEnemy
    {
        Patrol,
        Atack,
        IrDondeLabulla
    }
}
