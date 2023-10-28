using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    // [SerializeField] Transform frontRightTransform;
    // [SerializeField] Transform frontLeftTransform;
    // [SerializeField] Transform backRightTransform;
    // [SerializeField] Transform backleftTransform;

    public float acceleration = 500f;
    public float breakingForce = 600f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate() {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space)) {
            currentBreakForce = breakingForce;
        } else {
            currentBreakForce = 0f;
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        // UpdateWheel(frontLeft, frontLeftTransform);
        // UpdateWheel(backLeft, backleftTransform);
        // UpdateWheel(frontRight, frontRightTransform);
        // UpdateWheel(backRight, backRightTransform);
    }

    void  UpdateWheel(WheelCollider col, Transform trans) {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;

    }
}
