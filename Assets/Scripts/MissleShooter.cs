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

    private Animation anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject instMissle = Instantiate(misslePrefab);
            instMissle.transform.position = missleSpawn.position;
            instMissle.transform.forward = missleSpawn.forward;
            anim.Play();
        }
	}
}
