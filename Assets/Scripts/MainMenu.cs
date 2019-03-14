using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu: MonoBehaviour {

    public GUISkin skin;

    void OnGUI ()
    {

        GUI.skin = skin; 
        GUI.Label(new Rect(200, 10, 400, 100), "Captain Frank");

        if (GUI.Button(new Rect(10, 10, 100, 45), "Play"))
        {

            print("Ok Level 1 wird geladen...");

        }
        if (GUI.Button(new Rect(10, 65, 100, 45), "Quit"))
        {

            Application.Quit();

        }


    }

}
