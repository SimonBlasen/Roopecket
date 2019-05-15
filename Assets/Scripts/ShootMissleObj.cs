using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMissleObj : MonoBehaviour {

    [Header("Settings")]
    [SerializeField]
    private float thrustStrength = 10f;
    [SerializeField]
    private float thrustTime = 3f;
    [SerializeField]
    private float explosionStrength = 30f;

    [Header("References")]
    [SerializeField]
    private Transform thrustPos;
    [SerializeField]
    private ParticleSystem[] thrustParticles;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject explisionForce;


    private float thrustingFor = 0f;
    private Rigidbody ownRig;

	// Use this for initialization
	void Start ()
    {
        ownRig = GetComponent<Rigidbody>();
        //thrustParticles.Play();

    }

    private bool thrustersOff = false;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (thrustingFor < thrustTime)
        {
            thrustingFor += Time.deltaTime;

            ownRig.velocity += ownRig.velocity.normalized * thrustStrength;
            ownRig.angularVelocity = Vector3.zero;
            //ownRig.AddForceAtPosition(transform.up * thrustStrength, thrustPos.position);
        }
        else if (thrustersOff == false)
        {
            thrustersOff = true;
            for (int i = 0; i < thrustParticles.Length; i++)
            {
                thrustParticles[i].Stop();
            }

        }
	}

    private void explode()
    {
        List<Transform> affObjs = ExplosionAffectedList.Inst.AffectedObjects;

        for (int i = 0; i < affObjs.Count; i++)
        {
            affObjs[i].GetComponent<ExplosionAffectedObj>().Damage(explosionStrength / (Mathf.Pow(2f, Vector3.Distance(transform.position, affObjs[i].position))));
        }

        GameObject instExpl = Instantiate(explosionPrefab);
        instExpl.transform.position = transform.position;
        Destroy(instExpl, 4f);

        GameObject instExplF = Instantiate(explisionForce);
        instExplF.transform.position = transform.position;
        Destroy(instExplF, 4f);

        Debug.Log("Explode");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Rocket" && other.tag != "ExplosionForce")
        {
            Debug.Log(other.name);
            explode();
        }
    }
}
