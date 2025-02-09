using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingBehavior : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (patrolPoints.Length > 0)
        {
            MoveToNextPatrolPoint();
        }
        else
        {
            Debug.LogWarning("No patrol points assigned to " + gameObject.name);
        }
    }

    void Update()
    {
        // Check if we've reached the destination
        if (!agent.pathPending && agent.remainingDistance < 0f)
        {
            MoveToNextPatrolPoint();
        }
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPatrolIndex].position;

        // Move to the next patrol point, or loop back to the first one
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}