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

    [Header("Characteristic")]
    [SerializeField] private OidoEnemy oidoEnemy;
    private float patrolTimer = 0f;

    private void Reset()
    {
        patrolWaitTime = 2f;
        arriveDistance = 0.5f;
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        stateEnemy = StateEnemy.Patrol;
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
                break;
            case StateEnemy.Atack:
                break;
            case StateEnemy.IrDondeLabulla:
                DirectionPoint();
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
                GoToNextPoint();
            }
        }
    }

    private void GoToNextPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % points.Length;
        Destination(points[currentPointIndex].position);
    }
    private void DirectionPoint()
    {
    }

    private void Destination(Vector3 destination)
    {
        agent.SetDestination(new Vector3(destination.x,transform.position.y, destination.z));
    }

    private enum StateEnemy
    {
        Patrol,
        Atack,
        IrDondeLabulla
    }
    private void Ir(Vector3 position)
    {
        stateEnemy = StateEnemy.IrDondeLabulla;
        Destination(position);
    }
    private void OnDrawGizmos()
    {
        if(points==null) return;
        Gizmos.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawSphere(points[i].position, 0.1f);
        }
    }
    private void OnEnable()
    {
        oidoEnemy.OnCollisionEnter += Ir;
    }
    private void OnDisable()
    {
        oidoEnemy.OnCollisionEnter -= Ir;
    }
}
