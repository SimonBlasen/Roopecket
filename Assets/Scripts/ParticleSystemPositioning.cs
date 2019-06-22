using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPositioning : MonoBehaviour {

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Transform[] rockets;

    
    private RocketSpawner rs;

    // Use this for initialization
    void Start ()
    {
        rs = GameObject.FindObjectOfType<RocketSpawner>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rockets.Length > 0 && rockets[0] == null)
        {
            if (rs != null)
            {
                if (rs.SpawnedRocket != null)
                {
                    rockets = new Transform[] { rs.SpawnedRocket.transform };
                }
            }
            else
            {
                rockets = new Transform[] { GameObject.FindObjectOfType<RocketProps>().transform };
            }
        }
        else
        {
            Vector3 mid = Vector3.zero;


            for (int i = 0; i < rockets.Length; i++)
            {
                mid += rockets[i].position;
            }
            mid /= rockets.Length;

            transform.position = mid + offset;
        }


    }
}
