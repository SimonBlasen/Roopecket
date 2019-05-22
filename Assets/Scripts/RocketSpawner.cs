using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] rocketsPrefabs;


	// Use this for initialization
	void Start () {
        GameObject instRock = Instantiate(rocketsPrefabs[Statics.selectedRocket]);

        GameObject startPlatform = GameObject.Find("Start Platform");
        if (startPlatform == null)
        {
            Debug.LogError("[Hey Marc] Hab kei Anfangs-Lande Platform gfunde. Würdsch bitte eine mache? Grad so dass se \"Start Platform\" heißt. Merci gsaid");
        }
        else
        {
            CameraMultiController cmc = GameObject.FindObjectOfType<CameraMultiController>();

            instRock.GetComponent<RocketProps>().cameraMulti = cmc;

            SpawnedRocket = instRock;

            cmc.rockets = new Transform[] { instRock.transform };
            cmc.rocketRigidbody = instRock.GetComponent<Rigidbody>();

            instRock.transform.position = startPlatform.transform.position + new Vector3(0f, 2f, 0f);
            instRock.transform.Rotate(0f, startPlatform.transform.rotation.eulerAngles.y, 0f);


        }

    }


    public GameObject SpawnedRocket { get; set; }
	
	// Update is called once per frame
	void Update () {
		
	}
}
