using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControllerRemote : RocketController
{

    // Use this for initialization
    new void Start () {
		
	}

    // Update is called once per frame
    new void Update () {
		
	}
    new void FixedUpdate()
    {

    }

    

    public new bool LandingMoversOut
    {
        get
        {
            return landerMovers[0].TurnOut;
        }
        set
        {
            for (int i = 0; i < landerMovers.Length; i++)
            {
                landerMovers[i].TurnOut = value;
            }
        }
    }

    new void Init()
    {
        thrusts = new bool[thrustPositions.Length];
        audioOn = new bool[thrusts.Length];
        for (int i = 0; i < thrusts.Length; i++)
        {
            thrusts[i] = false;
        }
        for (int i = 0; i < audioOn.Length; i++)
        {
            audioOn[i] = false;
        }

        if (thrustStrengthes.Length == 0)
        {
            thrustStrengthes = new float[thrusts.Length];
            for (int i = 0; i < thrustStrengthes.Length; i++)
            {
                thrustStrengthes[i] = thrustStrength;
            }
        }

        Normal = new Vector3(0f, 0f, -1f);
        Turning = false;

        Manager.Instance.ActivateManager();
    }
}
