using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAffectedObj : MonoBehaviour {

    public float explosionHealth = -1f;

    private float currentHealth = 0f;

	// Use this for initialization
	void Start () {
        ExplosionAffectedList.Inst.AffectedObjects.Add(transform);
        currentHealth = explosionHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(float damage)
    {
        if (explosionHealth != -1f)
        {
            currentHealth -= damage;
            if (currentHealth <= 0f)
            {
                ExplosionAffectedList.Inst.AffectedObjects.Remove(transform);
                Destroy(gameObject);
            }
        }
    }
}
