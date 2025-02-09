using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendCommandBehavior : MonoBehaviour
{
    public Transform BoxTransform;
    public Transform StairsTransform;
    public Transform BarrelTransform;
    public NavMeshAgent NavMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToBox()
    {
        NavMeshAgent.SetDestination(BoxTransform.position);
        Debug.Log("MoveToBox");
    }

    public void MoveToStairs()
    {
        NavMeshAgent.SetDestination(StairsTransform.position);
        Debug.Log("MoveToStairs");
    }

    public void MoveToBarrel()
    {
        NavMeshAgent.SetDestination(BarrelTransform.position);
        Debug.Log("MoveToBarrel");
    }
}