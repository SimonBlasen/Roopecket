using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<Manager>();
                go.name = "Manager";
                instance = go.GetComponent<Manager>();
            }

            return instance;
        }
    }

    public void Landed(Transform rocket, string landingPlatform)
    {
        Debug.Log("Landed on " + landingPlatform);
    }

    public void Takeoff(Transform rocket, string landingPlatform)
    {
        Debug.Log("Took off from " + landingPlatform);
    }
}
