using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpitter : MonoBehaviour
{
    public GameObject ObjectToCreate;
    public Transform SpawnPoint;
    public float respawnTime = 1f;
    public float shootTime = 2f;


    // Start is called before the first frame update
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (shootTime > 0)
        {

            shootTime -= Time.deltaTime;
            if (shootTime <= 0)
            {
                SpawnObject();
                shootTime = respawnTime;
            }
              
                

        }
    }


    void SpawnObject()
    {
        Instantiate(ObjectToCreate, SpawnPoint.position, SpawnPoint.rotation);
    }
    
}
