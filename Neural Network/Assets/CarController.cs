using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSteerAngle = 42;

    public float motorForce = 800;

    public WheelCollider frontDriverCol, frontPassengerCol, backDriverCol, backPassengerCol;
    public Transform frontDriverTr, frontPassengerTr, backDriverTr, backPassengerTr;

    public float horizontalInput;
    public float verticalInput;

    //public int count;

    float steeringAngle;

    public Rigidbody rb;

    public Vector3 centerOfMassPos;

    void Start()
    {
        rb.centerOfMass = centerOfMassPos;

        /*
        for (int i = 0; i < count; i++)
        {
            Instantiate(gameObject, new Vector3(Random.Range(-5, 5), 15, 0), Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)));
        }
        */
    }

    void FixedUpdate()
    {
        Steer();
        Accelerate();
    }

    void Steer()
    {
        steeringAngle = horizontalInput * maxSteerAngle;

        frontDriverCol.steerAngle = steeringAngle;
        frontPassengerCol.steerAngle = steeringAngle;
    }

    void Accelerate()
    {
        backDriverCol.motorTorque = verticalInput * motorForce;
        backPassengerCol.motorTorque = verticalInput * motorForce;
    }

    void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverCol, frontDriverTr);
        UpdateWheelPose(frontPassengerCol, frontPassengerTr);
        UpdateWheelPose(backDriverCol, backDriverTr);
        UpdateWheelPose(backPassengerCol, backPassengerTr);
    }

    Vector3 pos;
    Quaternion quat;

    void UpdateWheelPose(WheelCollider wheelCollider, Transform tr)
    {
        pos = tr.position;
        quat = tr.rotation;

        wheelCollider.GetWorldPose(out pos, out quat);

        tr.position = pos;
        tr.rotation = quat;
    }
}
