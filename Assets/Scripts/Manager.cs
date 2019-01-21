using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance = null;

    private List<RocketProps> rocketsFueling = new List<RocketProps>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < rocketsFueling.Count; i++)
        {
            rocketsFueling[i].AddFuel(Time.deltaTime * 40f);
        }
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
        rocketsFueling.Add(rocket.GetComponent<RocketProps>());
        Debug.Log("Landed on " + landingPlatform);
    }

    public void Takeoff(Transform rocket, string landingPlatform)
    {
        rocketsFueling.Remove(rocket.GetComponent<RocketProps>());
        Debug.Log("Took off from " + landingPlatform);
    }
}
