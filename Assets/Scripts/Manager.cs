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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < rocketsFueling.Count; i++)
        {
            if (rocketsFueling[i] != null)
            {
                rocketsFueling[i].AddFuel(Time.deltaTime * 40f);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restart");
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

    public void Landed(Transform rocket, string landingPlatform)
    {
        rocketsFueling.Add(rocket.GetComponent<RocketProps>());

        if (landingPlatform.Split('_').Length >= 2 && landingPlatform.Split('_')[0] == "Finish")
        {

            string conc = landingPlatform.Split('_')[1];
            for (int i = 2; i < landingPlatform.Split('_').Length; i++)
            {
                conc += "_" + landingPlatform.Split('_')[i];
            }
        
            SavedGame.NextLevel[Statics.selectedRocket] = Statics.currentLevel + 1;

            timeKeeper.ReachedFinish();


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

        Debug.Log("Landed on " + landingPlatform);
    }

    public void Takeoff(Transform rocket, string landingPlatform)
    {
        rocketsFueling.Remove(rocket.GetComponent<RocketProps>());
        Debug.Log("Took off from " + landingPlatform);
    }
}
