using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControllerBehavior : MonoBehaviour
{
    public float Roll;
    public float Pitch;
    public float Yaw;
    public float DeadZone = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRotation (string Pippo)
    {
        String[] allDatasAsStrings = Pippo.Split('/');

        Roll = float.Parse(allDatasAsStrings[6], System.Globalization.CultureInfo.InvariantCulture);
        if (Roll < DeadZone && Roll > -DeadZone)
        {
            Roll = 0;
        }
        Pitch = float.Parse(allDatasAsStrings[7], System.Globalization.CultureInfo.InvariantCulture);
        if (Pitch < DeadZone && Pitch > -DeadZone)
        {
            Pitch = 0;
        }
        Yaw = float.Parse(allDatasAsStrings[8], System.Globalization.CultureInfo.InvariantCulture);
        if (Yaw < DeadZone && Yaw > -DeadZone)
        {
            Yaw = 0;
        }

        transform.eulerAngles = new Vector3(360 - Pitch,360 - Yaw,360 - Roll);
    }   
}
