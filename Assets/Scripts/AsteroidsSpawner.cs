using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public Transform minSpawnPos;
    public Transform maxSpawnPos;
    public float spawnWaitTime = 1f;
    public float minSpeed = 1f;
    public float maxSpeed = 1f;
    public float minSpeedAng = 1f;
    public float maxSpeedAng = 1f;
    public float lifeTime = 1f;
    public float drEberhardtProb = 0.05f;

    public GameObject[] asteroidsPrefabs;
    public GameObject asteroidDrEberhardt;

    private float spawnCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter += Time.deltaTime;
        
        if (spawnCounter >= spawnWaitTime)
        {
            spawnCounter = 0f;

            int astIndex = Random.Range(0, asteroidsPrefabs.Length);

            GameObject instAst = null;// Instantiate(asteroidsPrefabs[astIndex]);

            if (Random.Range(0f, 1f) <= drEberhardtProb)
            {
                instAst = Instantiate(asteroidDrEberhardt);
            }
            else
            {
                instAst = Instantiate(asteroidsPrefabs[astIndex]);
            }


            int side = Random.Range(0, 6);
            float rand0 = Random.Range(0f, 1f);
            float rand1 = Random.Range(0f, 1f);
            float rand2 = Random.Range(0f, 1f);
            float rand3 = Random.Range(0f, 1f);

            Vector3 spawnPos = Vector3.zero;
            Vector3 lookatPos = Vector3.zero;

            if (side == 0)
            {
                spawnPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand0 + minSpawnPos.position.x
                                    , minSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand1 + minSpawnPos.position.z);
                lookatPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand2 + minSpawnPos.position.x
                                    , maxSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand3 + minSpawnPos.position.z);
            }
            else if (side == 1)
            {
                spawnPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand0 + minSpawnPos.position.x
                                    , maxSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand1 + minSpawnPos.position.z);
                lookatPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand2 + minSpawnPos.position.x
                                    , minSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand3 + minSpawnPos.position.z);
            }
            else if (side == 2)
            {
                spawnPos = new Vector3(minSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand0 + minSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand1 + minSpawnPos.position.z);
                lookatPos = new Vector3(maxSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand2 + minSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand3 + minSpawnPos.position.z);
            }
            else if (side == 3)
            {
                spawnPos = new Vector3(maxSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand0 + minSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand1 + minSpawnPos.position.z);
                lookatPos = new Vector3(minSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand2 + minSpawnPos.position.y
                                    , (maxSpawnPos.position.z - minSpawnPos.position.z) * rand3 + minSpawnPos.position.z);
            }
            else if (side == 4)
            {
                spawnPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand0 + minSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand1 + minSpawnPos.position.y
                                    , minSpawnPos.position.z);
                lookatPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand2 + minSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand3 + minSpawnPos.position.y
                                    , maxSpawnPos.position.z);
            }
            else if (side == 5)
            {
                spawnPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand0 + minSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand1 + minSpawnPos.position.y
                                    , maxSpawnPos.position.z);
                lookatPos = new Vector3((maxSpawnPos.position.x - minSpawnPos.position.x) * rand2 + minSpawnPos.position.x
                                    , (maxSpawnPos.position.y - minSpawnPos.position.y) * rand3 + minSpawnPos.position.y
                                    , minSpawnPos.position.z);
            }

            instAst.transform.position = spawnPos;
            instAst.GetComponent<Rigidbody>().velocity = (lookatPos - spawnPos).normalized * Random.Range(minSpeed, maxSpeed);
            instAst.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(minSpeedAng, maxSpeedAng), Random.Range(minSpeedAng, maxSpeedAng), Random.Range(minSpeedAng, maxSpeedAng));

            for (int i = 0; i < instAst.transform.childCount; i++)
            {
                if (instAst.transform.GetChild(i).name == "Center")
                {
                    instAst.GetComponent<Rigidbody>().centerOfMass = instAst.transform.GetChild(i).localPosition;
                    break;
                }
            }

            Destroy(instAst, lifeTime / instAst.GetComponent<Rigidbody>().velocity.magnitude);
        }
    }
}
