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
    private ParticleSystem thrustParticles;


    private float thrustingFor = 0f;
    private Rigidbody ownRig;

	// Use this for initialization
	void Start ()
    {
        ownRig = GetComponent<Rigidbody>();
        //thrustParticles.Play();

    }
	
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
	}

    private void explode()
    {
        List<Transform> affObjs = ExplosionAffectedList.Inst.AffectedObjects;

        for (int i = 0; i < affObjs.Count; i++)
        {
            affObjs[i].GetComponent<ExplosionAffectedObj>().Damage(explosionStrength / (Mathf.Pow(2f, Vector3.Distance(transform.position, affObjs[i].position))));
        }
        Debug.Log("Explode");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Rocket")
        {
            explode();
        }
    }
}
