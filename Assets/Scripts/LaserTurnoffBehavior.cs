using UnityEngine;

public class LaserTurnoffBehavior : MonoBehaviour
{
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
            TurnOffLasers();
        }
    }

    private void TurnOffLasers()
    {
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("LASER");
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
}