using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpitter : MonoBehaviour
{
    public GameObject ObjectToCreate;
    public Transform SpawnPoint;
    public float respawnTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnObject();
            
        }
    }


    void SpawnObject()
    {
        Instantiate(ObjectToCreate, SpawnPoint.position, SpawnPoint.rotation);
    }
    
}
