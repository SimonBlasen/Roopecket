using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatformTrigger : MonoBehaviour {

    public LandingPlatform landingPlatform;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private int rocketInCount = 0;

    private void OnTriggerExit(Collider other)
    {
        Transform topParent = other.transform;
        while (topParent.parent != null)
        {
            topParent = topParent.parent;
        }

        if (landingPlatform.TriggerIsIn(topParent) && rocketInCount <= 1)
        {
            landingPlatform.TriggerExit(topParent);
        }
        
        if (rocketInCount > 0)
        {
            rocketInCount--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform topParent = other.transform;
        while (topParent.parent != null)
        {
            topParent = topParent.parent;
        }
        landingPlatform.TriggerEnter(topParent);
        rocketInCount++;
    }
}
