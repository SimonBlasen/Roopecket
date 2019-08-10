#if MOBILE
#else
using Steamworks;

#endif

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
        SceneLoadManager.Initialize();

#if MOBILE
#else
        if (SteamManager.Initialized)
        {
            switch (SteamApps.GetCurrentGameLanguage())
            {
                case "english":
                    LanguageManager.Language = Language.ENGLISH;
                    break;
                case "german":
                    LanguageManager.Language = Language.GERMAN;
                    break;
                case "spanish":
                    LanguageManager.Language = Language.SPANISH;
                    break;
                case "latam":
                    LanguageManager.Language = Language.SPANISH;
                    break;
                default:
                    LanguageManager.Language = Language.ENGLISH;
                    break;
            }
        }

#endif

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
