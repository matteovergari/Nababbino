using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyPatrollingBehavior : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;

    public float waitTime = 2f;
    public float rotationSpeed = 60f; // Degrees per second

    private bool isPatrolling = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            StartCoroutine(PatrolCoroutine());
        }
        else
        {
            Debug.LogWarning("No patrol points assigned to " + gameObject.name);
        }
    }

    IEnumerator PatrolCoroutine()
    {
        isPatrolling = true;

        while (isPatrolling)
        {
            if (patrolPoints.Length == 0)
                yield break;

            agent.destination = patrolPoints[currentPatrolIndex].position;

            while (agent.pathPending || agent.remainingDistance > 0.1f)
            {
                yield return null;
            }

            // Wait and rotate
            yield return StartCoroutine(WaitAndRotate());

            // Move to the next patrol point, or loop back to the first one
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
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
    }

    IEnumerator RotateCoroutine(float angle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;
        float rotationTime = Mathf.Abs(angle) / rotationSpeed;

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
        isPatrolling = false;
    }
}