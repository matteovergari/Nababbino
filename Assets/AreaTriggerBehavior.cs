using UnityEngine;

public class AreaTriggerBehavior : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
    }
}