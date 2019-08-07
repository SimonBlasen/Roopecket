using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSavegame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (SavedGame.firstStart)
            {
                SavedGame.firstStart = false;
                SavedGame.LoadSavegame();
                Statics.selectedRocket = SavedGame.OwnedRockets[SavedGame.LastPlayedRocket];
                if (Statics.selectedRocket == -1)
                {
                    Debug.LogError("Selected Rocket was -1");

                    SavedGame.FillWithInitValues();
                    SavedGame.FirstEverStart = true;


                    SceneManager.LoadScene("Main_Menu Difficulty");
                }
                else
                {
                    SceneManager.LoadScene("Main_Menu_3");
                }
            }
            else
            {
                SceneManager.LoadScene("Main_Menu_3");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Safe game wasn't loaded");
            SavedGame.FillWithInitValues();
            SavedGame.FirstEverStart = true;


            SceneManager.LoadScene("Main_Menu Difficulty");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
