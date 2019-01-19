using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProps : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float tickTime = 1f;
    [SerializeField]
    private int collisionDamage = 40;
    [SerializeField]
    private int maxHealth = 200;

    [Header("References")]
    [SerializeField]
    private Transform[] okToHitTransforms;

    private int currentHealth;

    private float tickCounter = 0f;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log("Died");
    }

    [Header("Shake Settings")]
    [SerializeField]
    private float magnitude = 2f;
    [SerializeField]
    private float roughness = 10f;
    [SerializeField]
    private float fadeIn = 0.1f;
    [SerializeField]
    private float fadeOut = 0.3f;

    public void Damage(int damage)
    {
        
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);


        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }




    private void OnCollisionStay(Collision collision)
    {
        bool notOkay = false;
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            bool thisCollisionIsOkay = false;
            for (int j = 0; j < okToHitTransforms.Length; j++)
            {
                if (collision.contacts[i].thisCollider.transform.GetInstanceID() == okToHitTransforms[j].GetInstanceID())
                {
                    thisCollisionIsOkay = true;
                    break;
                }
            }
            if (thisCollisionIsOkay == false)
            {
                notOkay = true;
                break;
            }
        }

        if (notOkay)
        {
            tickCounter += Time.deltaTime;
            if (tickCounter >= tickTime)
            {
                //Debug.Log("Collision stay");
                tickCounter = 0f;
                Damage(collisionDamage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool notOkay = false;
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            bool thisCollisionIsOkay = false;
            for (int j = 0; j < okToHitTransforms.Length; j++)
            {
                if (collision.contacts[i].thisCollider.transform.GetInstanceID() == okToHitTransforms[j].GetInstanceID())
                {
                    thisCollisionIsOkay = true;
                    break;
                }
            }
            if (thisCollisionIsOkay == false)
            {
                notOkay = true;
                break;
            }
        }

        if (notOkay)
        {
            //Debug.Log("Collision");
            tickCounter = 0f;
            Damage(collisionDamage);
        }
    }
}
