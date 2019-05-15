using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    public float explisionForce = 500f;

    private List<int> affected = new List<int>();

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnTriggerEnter(Collider other)
    {
        Transform par = other.transform;
        while (par.parent != null)
        {
            par = par.parent;
            if (par.GetComponent<Rigidbody>() != null)
            {
                break;
            }
        }
        if (affected.Contains(par.GetInstanceID()) == false && par.GetComponent<ShootMissleObj>() == null)
        {
            if (par.GetComponent<Rigidbody>() != null)
            {
                par.GetComponent<Rigidbody>().AddExplosionForce(explisionForce, transform.position, GetComponent<SphereCollider>().radius);
                Debug.Log("Added explision force to: " + par.name);
            }

            affected.Add(par.GetInstanceID());
        }
    }
}
