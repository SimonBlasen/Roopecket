﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour {

    [Header("The gravity in this zone. Default is (0, -1.7, 0)")]
    public Vector3 gravityHere = new Vector3(0f, -1.7f, 0f);

    [Space]

    [Header("Attention: Gravity zones are not allowed to overlap")]
    public bool nothingToSeeHere = false;

    // Use this for initialization
    void Start ()
    {
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 oldGravZone = Vector3.zero;
    private int instanceIn = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rocket")
        {
            Transform par = other.transform;
            while (par.parent != null)
            {
                par = par.parent;
            }

            if (par.GetInstanceID() != instanceIn)
            {
                instanceIn = par.GetInstanceID();
                oldGravZone = par.GetComponent<RocketController>().GravityZone;
                Debug.Log("Remember: " + oldGravZone.ToString());


                par.GetComponent<ConstantForce>().force = (-Physics.gravity + gravityHere) * par.GetComponent<Rigidbody>().mass;
                par.GetComponent<RocketController>().GravityZone = gravityHere;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rocket")
        {
            Transform par = other.transform;
            while (par.parent != null)
            {
                par = par.parent;
            }

            if (par.GetInstanceID() == instanceIn)
            {
                if (oldGravZone != new Vector3(200f, 200f, 200f))
                {
                    par.GetComponent<ConstantForce>().force = (-Physics.gravity + oldGravZone) * par.GetComponent<Rigidbody>().mass;
                    par.GetComponent<RocketController>().GravityZone = oldGravZone;
                }
                else
                {
                    par.GetComponent<ConstantForce>().force = Vector3.zero;
                    par.GetComponent<RocketController>().GravityZone = oldGravZone;
                }

                instanceIn = -1;

                Debug.Log("Reset to: " + oldGravZone.ToString());
            }

        }
    }
}
