using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] rocketsPrefabs;
    [SerializeField]
    private bool spawn2Rockets = false;

    public CameraMultiController cmc;
    public CameraMultiController cmc1;
    public CameraMultiController cmc2;
    private Camera cmcCam;
    private Camera cmc1Cam;
    private Camera cmc2Cam;
    private Transform rocket1;
    private Transform rocket2;


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

            //CameraMultiController cmc = GameObject.FindObjectOfType<CameraMultiController>();
            cmcCam = cmc.transform.GetComponentInChildren<Camera>();

            instRock.GetComponent<RocketProps>().cameraMulti = cmc;
            rocket1 = instRock.transform;

            SpawnedRocket = instRock;

            cmc.rockets = new Transform[] { instRock.transform };
            cmc.rocketRigidbody = instRock.GetComponent<Rigidbody>();

            instRock.transform.position = startPlatform.transform.position + new Vector3(0f, 2f, 0f);
            instRock.transform.Rotate(0f, startPlatform.transform.rotation.eulerAngles.y, 0f);


            if (spawn2Rockets)
            {
                instRock.GetComponent<RocketProps>().cameraMulti = cmc1;
                instRock2.GetComponent<RocketProps>().cameraMulti = cmc2;
                rocket2 = instRock2.transform;
                cmc.rockets = new Transform[] { instRock.transform, instRock2.transform };
                cmc.rocketRigidbody = null;
                instRock2.transform.position = startPlatformP2.transform.position + new Vector3(0f, 2f, 0f);
                instRock2.transform.Rotate(0f, startPlatformP2.transform.rotation.eulerAngles.y, 0f);

                instRock2.GetComponent<RocketController>().LandingKey = KeyCode.Keypad0;

                for (int i = 0; i < instRock2.GetComponent<RocketController>().Controls.Length; i++)
                {
                    if (i == 0)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.G;
                    }
                    else if (i == 1)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.H;
                    }
                    else if (i == 2)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.J;
                    }
                    else if (i == 3)
                    {
                        instRock2.GetComponent<RocketController>().Controls[i] = KeyCode.K;
                    }
                }

                
                cmc1.rockets = new Transform[] { instRock.transform };
                cmc1.rocketRigidbody = instRock.GetComponent<Rigidbody>();
                cmc1Cam = cmc1.transform.GetComponentInChildren<Camera>();
                cmc1.transform.GetComponentInChildren<AudioListener>().enabled = false;

                cmc2.rockets = new Transform[] { instRock2.transform };
                cmc2.rocketRigidbody = instRock2.GetComponent<Rigidbody>();
                cmc2Cam = cmc2.transform.GetComponentInChildren<Camera>();
                cmc2.transform.GetComponentInChildren<AudioListener>().enabled = false;

                cmc1Cam.enabled = false;
                cmc2Cam.enabled = false;
                cmcCam.enabled = true;
            }


        }

    }


    public GameObject SpawnedRocket { get; set; }
	
	// Update is called once per frame
	void Update ()
    {
		if (spawn2Rockets)
        {
            float angleDiff = absAngleDiff(rocket1.rotation.eulerAngles.y, rocket2.rotation.eulerAngles.y);

            if (angleDiff < 1f)
            {
                cmc1.LerpChild = false;
                cmc2.LerpChild = false;
                cmc1Cam.enabled = false;
                cmc2Cam.enabled = false;
                cmcCam.enabled = true;
                //cmc1Cam.transform.position = cmcCam.transform.position;
                //cmc2Cam.transform.position = cmcCam.transform.position;
            }
            else
            {
                //cmc1.LerpChild = true;
                //cmc2.LerpChild = true;
                cmc1Cam.enabled = true;
                cmc2Cam.enabled = true;
                cmcCam.enabled = false;
            }
        }
	}

    private float absAngleDiff(float angle1, float angle2)
    {
        if (Mathf.Abs(angle1 - angle2) >= 360f)
        {
            return absAngleDiff(angle1 > angle2 ? angle1 - 360f : angle1, angle1 > angle2 ? angle2 : angle2 - 360f);
        }
        else
        {
            return Mathf.Abs(angle1 - angle2);
        }
    }
}
