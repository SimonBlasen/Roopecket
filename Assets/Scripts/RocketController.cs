﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    [SerializeField]
    private KeyCode[] keyCodes;
    [SerializeField]
    private Transform[] thrustPositions;
    [SerializeField]
    private ParticleSystem[] thrustParticles;
    [SerializeField]
    private Light[] thrustLights;
    [SerializeField]
    private float thrustStrength = 10.0f;
    [SerializeField]
    private RocketProps rocketProps;

    private bool[] thrusts = null;

    private Rigidbody ownRig;

	// Use this for initialization
	void Start ()
    {
        Init();
	}

    private void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(ownRig.angularVelocity);
        localVelocity.x = 0f;
        localVelocity.y = 0f;

        ownRig.angularVelocity = transform.TransformDirection(localVelocity);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (rocketProps.OutOfFuel == false)
        {
            for (int i = 0; i < thrustPositions.Length; i++)
            {
                if (thrusts[i])
                {
                    ownRig.AddForceAtPosition(transform.up * thrustStrength, thrustPositions[i].position);
                }
            }

            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]))
                {
                    SetThrust(i, true);
                }
                else
                {
                    SetThrust(i, false);
                }
            }
        }
        else
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                SetThrust(i, false);
            }
        }


        if (!Turning)
        {
            float angleAbs = Vector3.Angle((new Vector3(0f, 0f, 1f)), transform.forward);
            if (Vector3.Angle((new Vector3(1f, 0f, 0f)), transform.forward) > 90f)
            {
                angleAbs = 360f - angleAbs;
            }

            angleAbs = angleAbs / 90f;
            while (angleAbs < 0f)
            {
                angleAbs += 4f;
            }
            angleAbs += 0.5f;
            angleAbs = ((int)angleAbs);
            angleAbs *= 90f;

            bool zNull = false;
            if ((angleAbs >= 45f && angleAbs < 135f) || (angleAbs >= 225f && angleAbs < 315f))
            {
                zNull = false;
            }

            transform.rotation = Quaternion.Euler(zNull ? transform.rotation.eulerAngles.x : 0f, angleAbs, zNull ? 0f : transform.rotation.eulerAngles.z);
        }
    }

    private void Init()
    {
        ownRig = GetComponent<Rigidbody>();
        thrusts = new bool[thrustPositions.Length];
        for (int i = 0; i < thrusts.Length; i++)
        {
            thrusts[i] = false;
        }

        Normal = new Vector3(0f, 0f, -1f);
        Turning = false;
    }

    public bool Turning
    {
        get;set;
    }

    public Vector3 Normal
    {
        get; set;
    }

    public bool[] Thrusts
    {
        get
        {
            return thrusts;
        }
    }

    public void SetThrust(int index, bool on)
    {
        if (thrusts == null)
        {
            Init();
        }
        if (index >= 0 && index < thrustPositions.Length)
        {
            if (on)
            {
                thrustLights[index].intensity = 5f;
                thrustParticles[index].Play();
            }
            else
            {
                thrustLights[index].intensity = 0f;
                thrustParticles[index].Stop();
            }
            thrusts[index] = on;
        }
    }
}
