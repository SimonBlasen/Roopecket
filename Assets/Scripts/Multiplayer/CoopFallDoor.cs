using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopFallDoor : CoopActivatable
{
    [SerializeField]
    private Rigidbody doorRig;
    [SerializeField]
    private HingeJoint hingeJoint;
    [SerializeField]
    private Transform fallDirection;
    [SerializeField]
    private Transform fallDestination;

    private float waitFall = 0f;
    private float velThresh = 1f;
    private float waitStandStill = 0f;

    private bool fallenDown = false;
    private bool fallingDown = false;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    new void Update ()
    {
        base.Update();

        if (fallingDown && fallenDown == false)
        {
            waitFall += Time.deltaTime;

            if (waitFall >= 2f)
            {
                if (Vector3.Angle(doorRig.transform.forward, fallDestination.forward) < velThresh
                    && Vector3.Angle(doorRig.transform.up, fallDestination.up) < velThresh)
                {
                    waitStandStill += Time.deltaTime;
                }
                else
                {
                    waitStandStill = 0f;
                }

                if (waitStandStill >= 0.4f)
                {
                    fallenDown = true;
                    Destroy(hingeJoint);
                    doorRig.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }
    }

    protected override void handleBoolValue(bool val)
    {
        if (fallingDown == false && val)
        {
            waitFall = 0f;
            fallingDown = true;
            doorRig.constraints = RigidbodyConstraints.None;
            doorRig.AddForceAtPosition(fallDirection.forward * 500f, fallDirection.position);
        }
    }
}
