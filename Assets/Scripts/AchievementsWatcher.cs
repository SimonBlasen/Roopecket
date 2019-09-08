using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementsWatcher : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private string oldScene = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Transform rocket = null;
    private float counter = 0f;
    private float counter2 = 0f;

    private float xLoop = 0f;
    private float oldX = 0f;
    private bool xPositive = true;
    private float zLoop = 0f;
    private float oldZ = 0f;
    private bool zPositive = true;

    // Update is called once per frame
    void Update()
    {
        if (rocket != null)
        {
            counter2 += Time.deltaTime;
            if (counter2 >= 0.2f)
            {
                counter2 = 0f;
                if (xLoop >= 360f || zLoop >= 360f)
                {
                    if (oldScene == "Tutorial1"
                        || oldScene == "Tutorial1.1"
                        || oldScene == "Tutorial1.2"
                        || oldScene == "Tutorial2"
                        || oldScene == "Tutorial2.1"
                        || oldScene == "Tutorial3"
                        || oldScene == "Tutorial4"
                        || oldScene == "Tutorial5")
                    {
                        if (SteamManager.Initialized)
                        {
                            SteamUserStats.SetAchievement("FLYING_A_LOOPING_IN_TUT");
                            Debug.Log("FLYING_A_LOOPING_IN_TUT");
                        }
                    }

                    if (SteamManager.Initialized)
                    {
                        SteamUserStats.SetAchievement("FLYING_A_LOOPING");
                        Debug.Log("FLYING_A_LOOPING");
                    }
                }


                if (Mathf.Abs(rocket.transform.eulerAngles.x - oldX) > 90f)
                {
                    if (rocket.transform.eulerAngles.x < oldX)
                    {
                        oldX -= 360f;
                    }
                    else
                    {
                        oldX += 360f;
                    }
                }

                if (rocket.transform.eulerAngles.x > oldX && xPositive)
                {
                    xLoop += Mathf.Abs(rocket.transform.eulerAngles.x - oldX);
                }
                else if (rocket.transform.eulerAngles.x > oldX && xPositive == false)
                {
                    xLoop = 0f;
                    xPositive = true;
                }
                else if (rocket.transform.eulerAngles.x < oldX && xPositive == false)
                {
                    xLoop += Mathf.Abs(rocket.transform.eulerAngles.x - oldX);
                }
                else
                {
                    xLoop = 0f;
                    xPositive = false;
                }

                oldX = rocket.transform.eulerAngles.x;





                if (Mathf.Abs(rocket.transform.eulerAngles.z - oldZ) > 90f)
                {
                    if (rocket.transform.eulerAngles.z < oldZ)
                    {
                        oldZ -= 360f;
                    }
                    else
                    {
                        oldZ += 360f;
                    }
                }

                if (rocket.transform.eulerAngles.z > oldZ && zPositive)
                {
                    zLoop += Mathf.Abs(rocket.transform.eulerAngles.z - oldZ);
                }
                else if (rocket.transform.eulerAngles.z > oldZ && zPositive == false)
                {
                    zLoop = 0f;
                    zPositive = true;
                }
                else if (rocket.transform.eulerAngles.z < oldZ && zPositive == false)
                {
                    zLoop += Mathf.Abs(rocket.transform.eulerAngles.z - oldZ);
                }
                else
                {
                    zLoop = 0f;
                    zPositive = false;
                }

                oldZ = rocket.transform.eulerAngles.z;
            }
            
        }






        counter += Time.deltaTime;
        if (counter >= 1f)
        {
            counter = 0f;

            if (SceneManager.GetActiveScene().name != oldScene)
            {
                oldScene = SceneManager.GetActiveScene().name;

                rocket = null;

                if (GameObject.FindObjectOfType<RocketSpawner>() != null)
                {
                    if (GameObject.FindObjectOfType<RocketSpawner>().SpawnedRocket != null)
                    {
                        rocket = GameObject.FindObjectOfType<RocketSpawner>().SpawnedRocket.transform;
                    }
                }
            }

            if (rocket == null && GameObject.FindObjectOfType<RocketSpawner>() != null)
            {
                if (GameObject.FindObjectOfType<RocketSpawner>().SpawnedRocket != null)
                {
                    rocket = GameObject.FindObjectOfType<RocketSpawner>().SpawnedRocket.transform;
                }
            }
            else if (rocket == null && GameObject.FindObjectOfType<RocketProps>() != null)
            {
                rocket = GameObject.FindObjectOfType<RocketProps>().transform;
            }
        }
    }
}
