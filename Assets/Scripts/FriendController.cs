using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCommandBehavior : MonoBehaviour
{
    public Transform BoxTransform;
    public Transform StairsTransform;
    public Transform BarrelTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToBox()
    {
        transform.position = BoxTransform.position;
    }

    public void MoveToStairs()
    {
        transform.position = StairsTransform.position;
    }

    public void MoveToBarrel()
    {
        transform.position = BarrelTransform.position;
    }
}