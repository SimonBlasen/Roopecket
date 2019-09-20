using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static Manager instance = null;

    private List<RocketProps> rocketsFueling = new List<RocketProps>();

    private TimeKeeper timeKeeper;

	// Use this for initialization
	void Start ()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial1"
            && SceneManager.GetActiveScene().name != "Tutorial1.1"
            && SceneManager.GetActiveScene().name !=  "Tutorial1.2"
            && SceneManager.GetActiveScene().name !=  "Tutorial2"
            && SceneManager.GetActiveScene().name !=  "Tutorial2.1"
            && SceneManager.GetActiveScene().name !=  "Tutorial3"
            && SceneManager.GetActiveScene().name !=  "Tutorial4"
            && SceneManager.GetActiveScene().name !=  "Tutorial5")
        {
            Cursor.visible = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < rocketsFueling.Count; i++)
        {
            if (rocketsFueling[i] != null)
            {
                rocketsFueling[i].AddFuel(Time.deltaTime * 40f);
                if (rocketsFueling[i].CurrentFuel >= rocketsFueling[i].MaxFuel)
                {
                    GetComponent<AudioSource>().Stop();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name != "Garage")
        {
            Debug.Log("Restart");
            //if (timeKeeper != null) Statics.resetMultiplier += timeKeeper.GetCurrentTime() * 0.04f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}

    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<Manager>();
                go.AddComponent<AudioSource>();
                go.GetComponent<AudioSource>().loop = true;
                go.name = "Manager";
                instance = go.GetComponent<Manager>();

                instance.timeKeeper = GameObject.FindObjectOfType<TimeKeeper>();
            }

            return instance;
        }
    }

    public void ActivateManager()
    {

    }

    private bool reachedFinish = false;

    public void Landed(Transform rocket, string landingPlatform)
    {
        while (rocket.parent != null)
        {
            rocket = rocket.parent;
        }

        rocketsFueling.Add(rocket.GetComponent<RocketProps>());

        if (SceneManager.GetActiveScene().name.StartsWith("Tutorial") == false)
        {
            GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<LevelNumber>().clipRefill;
            GetComponent<AudioSource>().Play();
        }

        if (landingPlatform.Split('_').Length >= 2 && landingPlatform.Split('_')[0] == "Finish" && reachedFinish == false)
        {
            reachedFinish = true;
            Cursor.visible = true;

            string conc = landingPlatform.Split('_')[1];
            for (int i = 2; i < landingPlatform.Split('_').Length; i++)
            {
                conc += "_" + landingPlatform.Split('_')[i];
            }
        
            if (Statics.isInFreestyle == false && SceneManager.GetActiveScene().name.StartsWith("Tutorial") == false && SceneManager.GetActiveScene().name.StartsWith("Test") == false)
            {
                SavedGame.NextLevel[SavedGame.LastPlayedRocket] = Statics.currentLevel + 1;
            }


            if (GameObject.FindObjectOfType<TutorialEndscreen>() != null)
            {
                GameObject.FindObjectOfType<TutorialEndscreen>().SceneToLoad = conc;
                GameObject.FindObjectOfType<TutorialEndscreen>().GetComponent<Canvas>().enabled = true;
                Cursor.visible = true;

                if (SceneManager.GetActiveScene().name == "Tutorial5")
                {
                    if (SteamManager.Initialized)
                    {
                        Steamworks.SteamUserStats.SetAchievement("PLAY_COMPLETE_TUT");
                    }
                }

                rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                rocket.GetComponent<RocketProps>().Indestroyable = true;
                rocket.GetComponent<RocketProps>().enabled = false;
                rocket.GetComponent<RocketController>().enabled = false;
            }
            else
            {
                timeKeeper.ReachedFinish();

                rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                rocket.GetComponent<RocketProps>().Indestroyable = true;
                rocket.GetComponent<RocketProps>().enabled = false;
                rocket.GetComponent<RocketController>().enabled = false;


                Statics.nextScene = conc;


                if (LevelNumber.GetStage(Statics.currentLevel) + 1 == LevelNumber.GetStage(Statics.currentLevel + 1))
                {
                    GameObject.FindObjectOfType<resultScreen>().showEndScreen();
                }
                else
                {
                    GameObject.FindObjectOfType<resultScreen>().showEndscreenSimple();
                    //SceneManager.LoadScene(conc);
                }
            }

        }

        Debug.Log("Landed on " + landingPlatform);
    }

    public void Takeoff(Transform rocket, string landingPlatform)
    {
        GetComponent<AudioSource>().Stop();
        rocketsFueling.Remove(rocket.GetComponent<RocketProps>());
        Debug.Log("Took off from " + landingPlatform);
    }
}
