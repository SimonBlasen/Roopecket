using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found_Prof : MonoBehaviour
{
    private bool foundProf = false;
    public GameObject profParticles, profParticles2;
    public int profNumber;


    private Camera cam;

    private void Start()
    {
        if (GameObject.FindObjectOfType<CameraMultiController>() != null)
        {
            cam = GameObject.FindObjectOfType<CameraMultiController>().GetComponentInChildren<Camera>();
        }
        else if (cam == null)
        {
            cam = Camera.main;
        }

        if (SteamManager.Initialized)
        {
            int val;
            SteamUserStats.GetStat("dreberhardt_found_" + profNumber.ToString(), out val);

            if (val == 1)
            {
                foundProf = true;
                profParticles.SetActive(true);
                profParticles2.SetActive(true);
                SavedGame.DrEberhardtFound[Statics.currentLevel] = true;
            }
            else
            {
                profParticles.SetActive(false);
            }
        }
        else
        {
            profParticles.SetActive(false);
        }
    }

    private float counter = 0f;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 3f)
        {
            counter = 0f;
            CheckDrEverhardtAchievements();
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (foundProf == false && Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetInstanceID() == transform.GetInstanceID())
                {
                    foundProf = true;
                    profParticles.SetActive(true);
                    profParticles2.SetActive(true);
                    SavedGame.DrEberhardtFound[Statics.currentLevel] = true;

                    if (SteamManager.Initialized)
                    {
                        SteamUserStats.SetStat("dreberhardt_found_" + profNumber, 1);
                        CheckDrEverhardtAchievements();
                    }
                }
            }
        }

    }

    public static void CheckDrEverhardtAchievements()
    {
        if (SteamManager.Initialized)
        {
            int drsFound = 0;
            for (int i = 0; i < 20; i++)
            {
                int val;
                SteamUserStats.GetStat("dreberhardt_found_" + i.ToString(), out val);
                if (val == 1)
                {
                    drsFound++;
                }
            }

            if (drsFound >= 1)
            {
                SteamUserStats.SetAchievement("FOUND_PROF_1");
            }
            if (drsFound >= 5)
            {
                SteamUserStats.SetAchievement("FOUND_PROF_5");
            }
            if (drsFound >= 10)
            {
                SteamUserStats.SetAchievement("FOUND_PROF_10");
            }
            if (drsFound >= 15)
            {
                SteamUserStats.SetAchievement("FOUND_PROF_15");
            }
            if (drsFound >= 20)
            {
                SteamUserStats.SetAchievement("FOUND_PROF_20");
            }
        }
    }
}
