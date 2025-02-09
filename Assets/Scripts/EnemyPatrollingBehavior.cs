using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyPatrollingBehavior : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator; // Add this reference
    private int currentPatrolIndex = 0;
    public Collider ChaseTrigger;
    public float ChaseDelay;
    public float ChaseTimer = 0f;
    public bool IsChasing = false;

    public float waitTime = 2f;
    public float rotationSpeed = 60f; // Degrees per second

    private enum EnemyState { Idle, Walking, Chasing }
    private EnemyState currentState = EnemyState.Idle;

    public void Update()
    {
        if (IsChasing == true)
        {
            StartCoroutine(ChaseCoroutine());
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Add this line
        ChaseTrigger.isTrigger = true;

        if (patrolPoints.Length > 0)
        {
            StartCoroutine(PatrolCoroutine());
        }
        else
        {
            Debug.LogWarning("No patrol points assigned to " + gameObject.name);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChaseTimer = Time.time;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time - ChaseTimer > ChaseDelay)
        {
            IsChasing = true;
            ChaseTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsChasing = false;
        }
    }

    IEnumerator PatrolCoroutine()
    {
        while (true)
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    yield return StartCoroutine(IdleCoroutine());
                    break;
                case EnemyState.Walking:
                    yield return StartCoroutine(WalkCoroutine());
                    break;
                case EnemyState.Chasing:
                    yield return StartCoroutine(ChaseCoroutine());
                    break;
            }
        }
    }

    IEnumerator IdleCoroutine()
    {
        currentState = EnemyState.Idle;
        agent.isStopped = true;
        animator.SetTrigger("Idle"); // Add this line

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        currentState = EnemyState.Walking;
    }

    IEnumerator WalkCoroutine()
    {
        currentState = EnemyState.Walking;
        agent.isStopped = false;
        animator.SetTrigger("Walk"); // Add this line
        agent.destination = patrolPoints[currentPatrolIndex].position;

        while (agent.pathPending || agent.remainingDistance > 0.1f)
        {
            yield return null;
        }

        // Wait and rotate
        yield return StartCoroutine(WaitAndRotate());

        // Move to the next patrol point, or loop back to the first one
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        currentState = EnemyState.Idle;
    }
    public void Chase()
    {
        StartCoroutine(ChaseCoroutine());
    }

    IEnumerator ChaseCoroutine()
    {
        currentState = EnemyState.Chasing;
        agent.isStopped = false;
        animator.SetTrigger("Chase");

        while (IsChasing && Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            agent.destination = target.position;
            yield return null;
        }

        currentState = EnemyState.Walking;
    }

    IEnumerator WaitAndRotate()
    {
        agent.isStopped = true;

        // Rotate 45 degrees to the right
        yield return StartCoroutine(RotateCoroutine(45f));

        // Rotate 90 degrees to the left (45 degrees past the starting position)
        yield return StartCoroutine(RotateCoroutine(-90f));

        // Rotate 45 degrees to the right (back to the starting position)
        yield return StartCoroutine(RotateCoroutine(45f));

        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        agent.isStopped = false;
        animator.SetTrigger("Idle");
    }

    IEnumerator RotateCoroutine(float angle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;
        float rotationTime = Mathf.Abs(angle) / rotationSpeed;
        animator.SetTrigger("Idle");

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}