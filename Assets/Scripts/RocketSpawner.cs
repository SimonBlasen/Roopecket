using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] rocketsPrefabs;
    [SerializeField]
    private GameObject[] rocketsRemotePrefabs;
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

    public float rectX = 0f;
    public float rectY = 0f;
    public float rectWidth = 1f;
    public float rectHeight = 1f;
    public float upRectX = 0f;
    public float upRectY = 0f;
    public float upRectWidth = 1f;
    public float upRectHeight = 1f;

    public GameObject canvasesSplited;
    public GameObject canvasesCombined;
    public GameObject canvasSafeFuel;
    public GameObject canvasManeuverHint;

    [SerializeField]
    private float seperateX = 100f;
    [SerializeField]
    private float seperateZ = 50f;


    public Transform[] StartPlatforms
    {
        get;
        protected set;
    }

    // Use this for initialization
    void Start ()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            Debug.Log("Detected controller");
            GameObject instController = new GameObject("Controller");
            instController.AddComponent<ControllerControl>();
        }

        GameObject instSafeFuel = Instantiate(canvasSafeFuel);
        GameObject instManeuver = Instantiate(canvasManeuverHint);

        GameObject mapSpawner = GameObject.Find("Map Spawner");
        if (mapSpawner != null)
        {
            mapSpawner.GetComponent<MapSpawner>().Init();
        }

        SpawnedRocket2 = null;
        GameObject instRock = Instantiate(rocketsPrefabs[Statics.selectedRocket]);
        GameObject instRock2 = null;
        if (spawn2Rockets)
        {
            instRock2 = Instantiate(rocketsPrefabs[Statics.selectedRocketP2]);
        }

        GameObject startPlatform = GameObject.Find("Start Platform");
        //StartPlatforms = new Transform[] { startPlatform.transform };
        GameObject startPlatformP2 = null;
        if (spawn2Rockets)
        {
            startPlatformP2 = GameObject.Find("Start Platform P2");
            //StartPlatforms = new Transform[] { startPlatform.transform, startPlatformP2.transform };
        }

        List<Transform> foundPlatforms = new List<Transform>();
        Transform foundPlat = startPlatform.transform;
        int cntPl = 2;
        while (foundPlat != null)
        {
            foundPlatforms.Add(foundPlat);
            GameObject objF = GameObject.Find("Start Platform P" + cntPl.ToString());
            if (objF != null)
            {
                foundPlat = objF.transform;
                cntPl++;
            }
            else
            {
                foundPlat = null;
            }
        }

        StartPlatforms = foundPlatforms.ToArray();

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
            if (spawn2Rockets == false)
            {
                cmc = GameObject.FindObjectOfType<CameraMultiController>();
            }
            cmcCam = cmc.transform.GetComponentInChildren<Camera>();

            instRock.GetComponent<RocketProps>().cameraMulti = cmc;
            instSafeFuel.GetComponent<TextBlinkScr>().rocketController = instRock.GetComponent<RocketController>();
            instManeuver.GetComponent<TextBlinkSrcManeuver>().rocketController = instRock.GetComponent<RocketController>();

            Transform cameraTarget = instRock.GetComponent<RocketController>().cameraPoint;

            rocket1 = instRock.transform;

            SpawnedRocket = instRock;

            cmc.rockets = new Transform[] { cameraTarget != null ? cameraTarget : instRock.transform };
            cmc.rocketRigidbody = instRock.GetComponent<Rigidbody>();

            instRock.transform.position = startPlatform.transform.position + new Vector3(0f, 2.5f + (Statics.selectedRocket == 4 ? 1.0f : 0f), 0f);
            instRock.transform.Rotate(0f, startPlatform.transform.rotation.eulerAngles.y, 0f);
            if (GameObject.FindObjectOfType<LevelNumber>() != null && GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp == 19)
            {
                instRock.transform.Rotate(0f, 0f, 180f);
            }
            if (Mathf.Cos(startPlatform.transform.rotation.eulerAngles.y * Mathf.PI / 180f) > -0.5f && Mathf.Cos(startPlatform.transform.rotation.eulerAngles.y * Mathf.PI / 180f) < 0.5f)
            {
                instRock.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                instRock.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            }

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

                if (Mathf.Cos(startPlatform.transform.rotation.eulerAngles.y * Mathf.PI / 180f) > -0.5f && Mathf.Cos(startPlatform.transform.rotation.eulerAngles.y * Mathf.PI / 180f) < 0.5f)
                {
                    instRock2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                {
                    instRock2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                }


                SpawnedRocket2 = instRock2;

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

                cmc1.vector1GoalOutside = new Vector2(rectX, rectY);
                cmc1.vector2GoalOutside = new Vector2(rectWidth, rectHeight);


                cmc2.rockets = new Transform[] { instRock2.transform };
                cmc2.rocketRigidbody = instRock2.GetComponent<Rigidbody>();
                cmc2Cam = cmc2.transform.GetComponentInChildren<Camera>();
                cmc2.transform.GetComponentInChildren<AudioListener>().enabled = false;

                cmc2.vector1GoalOutside = new Vector2(upRectX, upRectY);
                cmc2.vector2GoalOutside = new Vector2(upRectWidth, upRectHeight);

                cmc1Cam.enabled = false;
                cmc2Cam.enabled = false;
                cmcCam.enabled = true;
            }


        }

    }


    public GameObject SpawnedRocket { get; set; }
    public GameObject SpawnedRocket2 { get; set; }

    private bool camsSepBefore = false;
    private bool lerpedBack = false;

    // Update is called once per frame
    void Update ()
    {
		if (spawn2Rockets)
        {
            float angleDiff = absAngleDiff(rocket1.rotation.eulerAngles.y, rocket2.rotation.eulerAngles.y);

            //Debug.Log(angleDiff);

            if (shouldMerge() && camsSepBefore)
            {
                Debug.Log("1");
                camsSepBefore = false;

                cmc1.LerpChild = false;
                cmc2.LerpChild = false;
                cmc1.finishedLerpProj = false;
                cmc2.finishedLerpProj = false;
                cmc1.LerpToTransform(cmcCam.transform);
                cmc2.LerpToTransform(cmcCam.transform);

                lerpedBack = false;
            }
            else if (shouldMerge() && lerpedBack == false && cmc1.FinishedLerpingBack && cmc2.FinishedLerpingBack)
            {
                Debug.Log("2");
                lerpedBack = true;
                cmc1Cam.enabled = false;
                cmc2Cam.enabled = false;
                cmcCam.enabled = true;
                canvasesCombined.SetActive(true);
                canvasesSplited.SetActive(false);
                cmc1Cam.transform.position = cmcCam.transform.position;
                cmc2Cam.transform.position = cmcCam.transform.position;
            }
            else if (shouldSeperate() && camsSepBefore == false)
            {
                Debug.Log("3");
                camsSepBefore = true;
                cmc1.LerpChild = true;
                cmc2.LerpChild = true;
                cmc1.finishedLerpProj = false;
                cmc2.finishedLerpProj = false;
                cmc1.timeLerpBackToTransform = 0f;
                cmc2.timeLerpBackToTransform = 0f;

                CameraMultiController.SetScissorRect(cmc1Cam, new Rect(rectX, rectY, rectWidth, rectHeight));
                CameraMultiController.SetScissorRect(cmc2Cam, new Rect(upRectX, upRectY, upRectWidth, upRectHeight));
                cmc1.proj1 = new Vector2(rectX, rectY);
                cmc1.proj2 = new Vector2(rectWidth, rectHeight);
                cmc2.proj1 = new Vector2(upRectX, upRectY);
                cmc2.proj2 = new Vector2(upRectWidth, upRectHeight);

                cmc1Cam.enabled = true;
                cmc2Cam.enabled = true;
                cmcCam.enabled = false;
                canvasesCombined.SetActive(false);
                canvasesSplited.SetActive(true);
            }
        }
	}

    private bool shouldSeperate()
    {
        float angleDiff = absAngleDiff(rocket1.rotation.eulerAngles.y, rocket2.rotation.eulerAngles.y);


        float xDiff = Mathf.Abs(rocket1.position.x - rocket2.position.x);
        float yDiff = Mathf.Abs(rocket1.position.y - rocket2.position.y);
        float zDiff = Mathf.Abs(rocket1.position.z - rocket2.position.z);

        float diffXPlane = 0f;
        float diffZPlane = 0f;

        float an = absAngleDiff(0f, rocket1.rotation.eulerAngles.y);
        if (an < 45f || (an > 135f && an < 225f) || an > 315f)
        {
            diffXPlane = xDiff * xDiff + yDiff * yDiff;
            diffZPlane = zDiff * zDiff + yDiff * yDiff;
        }
        else
        {
            diffXPlane = zDiff * zDiff + yDiff * yDiff;
            diffZPlane = xDiff * xDiff + yDiff * yDiff;
        }

        if (angleDiff > 1f || diffXPlane >= seperateX || diffZPlane >= seperateZ)
        {
            return true;
        }
        return false;
    }

    private bool shouldMerge()
    {
        float angleDiff = absAngleDiff(rocket1.rotation.eulerAngles.y, rocket2.rotation.eulerAngles.y);


        float xDiff = Mathf.Abs(rocket1.position.x - rocket2.position.x);
        float yDiff = Mathf.Abs(rocket1.position.y - rocket2.position.y);
        float zDiff = Mathf.Abs(rocket1.position.z - rocket2.position.z);

        float diffXPlane = 0f;
        float diffZPlane = 0f;

        float an = absAngleDiff(0f, rocket1.rotation.eulerAngles.y);
        if (an < 45f || (an > 135f && an < 225f) || an > 315f)
        {
            diffXPlane = xDiff * xDiff + yDiff * yDiff;
            diffZPlane = zDiff * zDiff + yDiff * yDiff;
        }
        else
        {
            diffXPlane = zDiff * zDiff + yDiff * yDiff;
            diffZPlane = xDiff * xDiff + yDiff * yDiff;
        }

        if (angleDiff < 0.2f && diffXPlane < seperateX && diffZPlane < seperateZ)
        {
            return true;
        }
        return false;
    }

    public Transform SpawnrocketNoRig(short rocketID)
    {
        GameObject instRock = Instantiate(rocketsRemotePrefabs[rocketID]);
        return instRock.transform;
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

    public bool Spawn2Rockets
    {
        get
        {
            return spawn2Rockets;
        }
    }
}
