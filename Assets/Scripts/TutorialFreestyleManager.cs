using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFreestyleManager : MonoBehaviour
{
    private RocketProps rocketProps;
    private RocketSpawner rs;

    // Start is called before the first frame update
    void Start()
    {
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketProps == null)
        {
            if (rs != null)
            {
                if (rs.SpawnedRocket != null)
                {
                    rocketProps = rs.SpawnedRocket.GetComponent<RocketProps>();
                    rocketProps.MaxFuel *= 20f;
                    Debug.Log("Max fuel altered");
                }
            }
            else
            {
                rocketProps = GameObject.FindObjectOfType<RocketProps>();
            }
        }
    }
}
