using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderMover : MonoBehaviour {

    public float angleIn;
    public float angleOut;
    public float epsilon = 0.01f;
    public float rotationSpeed = 10f;
    public bool turnDirection = false;

    public bool oldVersion = false;

    public Transform arm;

    public HingeJoint[] joints;
    public float[] velocities;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (oldVersion)
        {
            if (Mathf.Abs(angleIn - arm.localRotation.eulerAngles.z) > epsilon && turnedOut == false)
            {
                float diff = Mathf.Abs(angleIn - arm.localRotation.eulerAngles.z);

                arm.localRotation = Quaternion.Euler(arm.localRotation.eulerAngles.x, 0f, arm.localRotation.eulerAngles.z + (turnDirection ? 1f : -1f) * rotationSpeed * Time.deltaTime);

                float diff2 = Mathf.Abs(angleIn - arm.localRotation.eulerAngles.z);

                if (diff2 > diff)
                {
                    arm.localRotation = Quaternion.Euler(arm.localRotation.eulerAngles.x, 0f, angleIn);
                }
            }
            else if (Mathf.Abs(angleOut - arm.localRotation.eulerAngles.z) > epsilon && turnedOut)
            {
                float diff = Mathf.Abs(angleOut - arm.localRotation.eulerAngles.z);

                arm.localRotation = Quaternion.Euler(arm.localRotation.eulerAngles.x, 0f, arm.localRotation.eulerAngles.z + (turnDirection ? -1f : 1f) * rotationSpeed * Time.deltaTime);

                float diff2 = Mathf.Abs(angleOut - arm.localRotation.eulerAngles.z);

                if (diff2 > diff)
                {
                    arm.localRotation = Quaternion.Euler(arm.localRotation.eulerAngles.x, 0f, angleOut);
                }
            }
        }
        /*
		
        */
    }

    private bool gettingCloser = false;

    private bool turnedOut = true;

    public bool TurnOut
    {
        get
        {
            return turnedOut;
        }
        set
        {
            turnedOut = value;

            if (!oldVersion)
            {
                for (int i = 0; i < joints.Length; i++)
                {
                    JointMotor jm = joints[i].motor;
                    jm.targetVelocity = velocities[i] * (turnedOut ? 1f : -1f);
                    joints[i].motor = jm;
                }
            }
        }
    }
}
