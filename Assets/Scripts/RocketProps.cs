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
    private float collisionDamage = 40;
    [SerializeField]
    private float impactToleranceThrusters = 5f;
    [SerializeField]
    private int maxHealth = 200;
    [SerializeField]
    private float maxFuel = 200f;
    [SerializeField]
    private float maxFuelExtra = 200f;
    [SerializeField]
    private float thrustFuelPerSecond = 4f;
    [SerializeField]
    private float thrustFuelPerSecondExtra = 4f;
    [SerializeField]
    public bool[] usesExtraFuel;

    [Header("References")]
    [SerializeField]
    private Transform[] okToHitTransforms;
    [SerializeField]
    private RocketController rocketController;
    [SerializeField]
    public CameraMultiController cameraMulti;
    [SerializeField]
    private AudioClip damageAudio;
    [SerializeField]
    private AudioSource damageAudioSource;
    [SerializeField]
    private float damageAudioVolume;
    [SerializeField]
    private AudioClip lowHealthClip;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject dieExplosion;
    [SerializeField]
    private GameObject deadRocket;
    [SerializeField]
    private GameObject dieExplosionForce;
    public GameObject damageSmoke;
    public GameObject damageFire;

    private int currentHealth;

    private float currentFuel = 0f;
    private float currentFuelExtra = 0f;

    private float tickCounter = 0f;

    // Use this for initialization
    void Start()
    {
        damageAudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        if (damageSmoke != null) damageSmoke.SetActive(false);
        currentFuel = maxFuel;
        currentFuelExtra = maxFuelExtra;
        if (cameraMulti != null)
        {
            cameraMulti.IsRocketDead = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFuel > 0f)
        {
            for (int i = 0; i < rocketController.Thrusts.Length; i++)
            {
                if (rocketController.Thrusts[i] > 0f)
                {
                    float factor = 1f;
                    if (rocketController.LandingMoversOut && usesExtraFuel[i] == false)
                    {
                        factor = 4f;
                    }
                    if (usesExtraFuel[i])
                    {
                        currentFuelExtra -= Time.deltaTime * thrustFuelPerSecondExtra * factor * rocketController.Thrusts[i];
                    }
                    else
                    {
                        currentFuel -= Time.deltaTime * thrustFuelPerSecond * factor * rocketController.Thrusts[i];
                    }
                    StaticsSingleplayer.UseFuel(Time.deltaTime * thrustFuelPerSecond * factor);
                }
            }
        }
        else
        {
            currentFuel = 0f;
        }
    }

    public bool Indestroyable
    {
        get;set;
    }

    public bool IsDestroyed
    {
        get
        {
            return currentHealth <= 0;
        }
    }

    public bool OutOfFuel
    {
        get
        {
            return currentFuel <= 0f;
        }
    }

    public bool OutOfFuelExtra
    {
        get
        {
            return currentFuelExtra <= 0f;
        }
    }

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public float CurrentFuel
    {
        get
        {
            return currentFuel;
        }
    }

    public float CurrentFuelExtra
    {
        get
        {
            return currentFuelExtra;
        }
    }

    public float MaxFuel
    {
        get
        {
            return maxFuel;
        }
        set
        {
            maxFuel = value;
            currentFuel = maxFuel;
        }
    }

    public float MaxFuelExtra
    {
        get
        {
            return maxFuelExtra;
        }
        set
        {
            maxFuelExtra = value;
            currentFuelExtra = maxFuelExtra;
        }
    }

    public void AddFuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    public void Die()
    {
        if (!Indestroyable)
        {
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //Debug.Log("Died");
            GameObject explosion = Instantiate(dieExplosion);
            explosion.transform.position = transform.position;

            GameObject deadRocketInst = Instantiate(deadRocket);
            deadRocketInst.transform.position = transform.position;
            deadRocketInst.transform.rotation = transform.rotation;

            cameraMulti.IsRocketDead = true;

            Destroy(explosion, 3.8f);

            GameObject instForce = Instantiate(dieExplosionForce);
            instForce.transform.position = transform.position;

            Destroy(instForce, 2f);

            gameObject.SetActive(false);
        }
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

    private GameObject instAudioLowHealth = null;

    float addHP = 0f;

    public void Heal(float health)
    {
        addHP += health;
        while (addHP > 1f)
        {
            currentHealth += 1;
            addHP -= 1f;
        }

        if (currentHealth > (maxHealth / 2))
        {

            damageSmoke.SetActive(false);

        }
        if (currentHealth > ((maxHealth / 2) - (maxHealth / 4)) && currentHealth <= (maxHealth / 2))
        {
            GameObject goLowH = GameObject.Find("Low Health Audio");

            if (goLowH != null)
            {
                Destroy(goLowH);
            }

            damageFire.SetActive(false);


        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Damage(int damage)
    {
        if (!Indestroyable)
        {
            CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);

            StaticsSingleplayer.Inst.AddDamage(damage);

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }


            if (currentHealth <= (maxHealth / 2))
            {

                damageSmoke.SetActive(true);

            }
            if (currentHealth <= ((maxHealth / 2) - (maxHealth / 4)))
            {
                if (instAudioLowHealth == null)
                {
                    instAudioLowHealth = new GameObject("Low Health Audio");
                    instAudioLowHealth.AddComponent<AudioSource>();
                    instAudioLowHealth.GetComponent<AudioSource>().clip = lowHealthClip;
                    instAudioLowHealth.GetComponent<AudioSource>().volume = 1.5f;
                    instAudioLowHealth.GetComponent<AudioSource>().Play();
                }

                damageFire.SetActive(true);

            }
        }
    }



    private void OnCollisionStay(Collision collision)
    {/*
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
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Impact: " + collision.impulse.magnitude.ToString());
       
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

        Transform topParent = collision.transform;
        while (topParent.parent != null)
        {
            topParent = topParent.parent;
        }

        if (topParent.GetInstanceID() != transform.GetInstanceID())
        {
            if (notOkay)
            {
                //Debug.Log("Collision");
                //tickCounter = 0f;
                Damage((int)(collisionDamage * collision.impulse.magnitude));

            }
            else
            {
                if (collision.impulse.magnitude > impactToleranceThrusters)
                {
                    FindObjectOfType<AudioManager>().Play("DamageSound");
                    Damage((int)(collisionDamage * (collision.impulse.magnitude - impactToleranceThrusters)));
                }
            }
        }

    }
}
