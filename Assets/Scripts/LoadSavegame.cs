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
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Safe game wasn't loaded");
            SavedGame.FillWithInitValues();
        }

        SceneManager.LoadScene("Main_Menu_3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
