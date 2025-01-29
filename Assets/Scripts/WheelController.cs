using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;
    [SerializeField] private float maxMotorTorque = 500f;
    [SerializeField] private float maxSteerAngle = 30f;

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steer = maxSteerAngle * Input.GetAxis("Horizontal");

        frontLeftWheel.motorTorque = motor;
        frontRightWheel.motorTorque = motor;
        backLeftWheel.motorTorque = motor;
        backRightWheel.motorTorque = motor;

        frontLeftWheel.steerAngle = steer;
        frontRightWheel.steerAngle = steer;
    }
}