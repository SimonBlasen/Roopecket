using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle291 : Obstacle
{
    [Header("Settings")]
    [SerializeField]
    private float minTimeIdle = 1f;
    [SerializeField]
    private float maxTimeIdle = 7f;
    [SerializeField]
    private float minTimeActive = 3f;
    [SerializeField]
    private float maxTimeActive = 5f;
    [SerializeField]
    private float blindDuration = 4f;
    [SerializeField]
    private float splashSpawnCDMin = 0.1f;
    [SerializeField]
    private float splashSpawnCDMax = 0.4f;
    [SerializeField]
    private float splashDurationMin = 2f;
    [SerializeField]
    private float splashDurationMax = 5f;

    [Header("References")]
    [SerializeField]
    private ParticleSystem partSystem;
    [SerializeField]
    private BoostCollider colliderRange;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject prefabCanvasSplashed;


    private Manager manager = null;
    private Singleplayer singleplayer = null;
    private SplashCanvas splashCanvas = null;


    private bool currentlyActive = false;
    private float randomDuration = 0f;
    private float counter = 0f;
    private int seed = 0;
    private float spawnCD = 0f;
    private System.Random random;

    // Use this for initialization
    void Start ()
    {
        partSystem.Play();
        if (GameObject.FindObjectOfType<SplashCanvas>() == null)
        {
            GameObject inst = Instantiate(prefabCanvasSplashed);
            splashCanvas = inst.GetComponent<SplashCanvas>();
        }
        else
        {
            splashCanvas = GameObject.FindObjectOfType<SplashCanvas>();
        }

        if (GameObject.Find("Manager") != null)
        {
            manager = GameObject.Find("Manager").GetComponent<Manager>();
        }
        else if (GameObject.Find("Singleplayer Manager") != null)
        {
            manager = null;
            singleplayer = GameObject.Find("Singleplayer Manager").GetComponent<Singleplayer>();
        }

        random = new System.Random(seed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        counter += Time.deltaTime;

        if (counter >= randomDuration)
        {
            counter -= randomDuration;

            CurrentlyActive = !CurrentlyActive;
            
            if (CurrentlyActive == false)
            {
                Debug.Log("Out");
                randomDuration = random.Next((int)(minTimeIdle * 100f), (int)(maxTimeIdle * 100f));
                randomDuration /= 100f;
            }
            else
            {
                Debug.Log("Active");
                randomDuration = random.Next((int)(minTimeActive * 100f), (int)(maxTimeActive * 100f));
                randomDuration /= 100f;
            }
        }


        if (CurrentlyActive && colliderRange.PlayerIn)
        {
            spawnCD -= Time.deltaTime;
            if (spawnCD <= 0f)
            {
                spawnCD = Random.Range(splashSpawnCDMin, splashSpawnCDMax);

                splashCanvas.SpawnInc(Random.Range(splashDurationMin, splashDurationMax));
            }
        }
	}

    private bool CurrentlyActive
    {
        get
        {
            return currentlyActive;
        }
        set
        {
            currentlyActive = value;

            if (currentlyActive && partSystem.isPlaying == false)
            {
                partSystem.Play();
            }
            else if (currentlyActive == false && partSystem.isPlaying)
            {
                //partSystem.Stop();
            }
        }

    }

    public override void GameStart()
    {
        if (manager != null)
        {
            seed = manager.PseudoRandomSeed;
            counter = 0f;
            CurrentlyActive = false;
            random = new System.Random(seed);

            if (CurrentlyActive == false)
            {
                randomDuration = random.Next((int)(minTimeIdle * 100f), (int)(maxTimeIdle * 100f));
                randomDuration /= 100f;
            }
            else
            {
                randomDuration = random.Next((int)(minTimeActive * 100f), (int)(maxTimeActive * 100f));
                randomDuration /= 100f;
            }
        }
    }
}
