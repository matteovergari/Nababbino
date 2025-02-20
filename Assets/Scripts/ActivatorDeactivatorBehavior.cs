using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDeactivatorBehavior : MonoBehaviour
{
    public GameObject targetObjectToActivate;
    public GameObject targetObjectToDeactivate;
    public GameObject targetObjectToDeactivate2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetObjectToActivate != null)
            {
                targetObjectToActivate.SetActive(true);
            }

            if (targetObjectToDeactivate != null)
            {
                targetObjectToDeactivate.SetActive(false);
                targetObjectToDeactivate2.SetActive(false);
            }
        }
    }
}