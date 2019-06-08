using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] rocketsPrefabs;
    [SerializeField]
    private bool spawn2Rockets = false;


    // Use this for initialization
    void Start () {
        GameObject instRock = Instantiate(rocketsPrefabs[Statics.selectedRocket]);
        GameObject instRock2 = null;
        if (spawn2Rockets)
        {
            instRock2 = Instantiate(rocketsPrefabs[Statics.selectedRocketP2]);
        }

        GameObject startPlatform = GameObject.Find("Start Platform");
        GameObject startPlatformP2 = null;
        if (spawn2Rockets)
        {
            startPlatformP2 = GameObject.Find("Start Platform P2");
        }
        if (startPlatform == null)
        {
            Debug.LogError("[Hey Marc] Hab kei Anfangs-Lande Platform gfunde. Würdsch bitte eine mache? Grad so dass se \"Start Platform\" heißt. Merci gsaid");
        }
        else
        {
            /*if (GameObject.FindObjectOfType<Canvas>() == null)
            {
                Debug.LogError("No Canvas found");
            }
            else
            {
                if (GameObject.FindObjectOfType<Canvas>().transform.GetComponentInChildren<UIRocketGreenFill>() == null)
                {
                    Debug.LogError("Could not find health bar gui");
                }
                else
                {
                    GameObject.FindObjectOfType<Canvas>().transform.GetComponentInChildren<UIRocketGreenFill>()
                }
            }*/

            CameraMultiController cmc = GameObject.FindObjectOfType<CameraMultiController>();

            instRock.GetComponent<RocketProps>().cameraMulti = cmc;

            SpawnedRocket = instRock;

            cmc.rockets = new Transform[] { instRock.transform };
            cmc.rocketRigidbody = instRock.GetComponent<Rigidbody>();

            instRock.transform.position = startPlatform.transform.position + new Vector3(0f, 2f, 0f);
            instRock.transform.Rotate(0f, startPlatform.transform.rotation.eulerAngles.y, 0f);


            if (spawn2Rockets)
            {
                instRock2.GetComponent<RocketProps>().cameraMulti = cmc;
                cmc.rockets = new Transform[] { instRock.transform, instRock2.transform };
                cmc.rocketRigidbody = null;
                instRock2.transform.position = startPlatformP2.transform.position + new Vector3(0f, 2f, 0f);
                instRock2.transform.Rotate(0f, startPlatformP2.transform.rotation.eulerAngles.y, 0f);

                instRock2.GetComponent<RocketController>().LandingKey = KeyCode.Keypad0;

                for (int i = 0; i < instRock2.GetComponent<RocketController>().Controls.Length; i++)
                {
                    if (i == 0)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.Keypad4;
                    }
                    else if (i == 1)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.Keypad5;
                    }
                    else if (i == 2)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.Keypad6;
                    }
                    else if (i == 3)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.KeypadPlus;
                    }
                }

            }


        }

    }


    public GameObject SpawnedRocket { get; set; }
	
	// Update is called once per frame
	void Update () {
		
	}
}
