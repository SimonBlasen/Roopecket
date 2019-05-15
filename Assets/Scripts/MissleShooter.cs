using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleShooter : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject misslePrefab;

    [Header("References")]
    [SerializeField]
    private Transform missleSpawn;
    [SerializeField]
    private Rigidbody rocketRig;

    public Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    private float waitMissleAnim = 0f;

	// Update is called once per frame
	void Update ()
    {
        waitMissleAnim -= Time.deltaTime;
        if (waitMissleAnim <= 0f && waitMissleAnim >= -111f)
        {
            waitMissleAnim = -500f;
            anim.SetBool("open", false);
        }
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject instMissle = Instantiate(misslePrefab);
            instMissle.transform.position = missleSpawn.position;
            instMissle.transform.forward = missleSpawn.forward;
            instMissle.GetComponent<Rigidbody>().velocity = rocketRig.velocity + (missleSpawn.forward * 3f);
            anim.SetBool("open", true);
            waitMissleAnim = 0.3f;
        }


	}
}
