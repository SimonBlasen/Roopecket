using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity_calculator : MonoBehaviour {

    public string gravity = "-1";
    public GUISkin skin;

    // Use this for initialization
    void Start () {
		
	}

    private void OnGUI()
    {
        GUI.skin = skin;

        GUI.Label(new Rect (20,20,200,100), "Gravity: " + gravity);

    }
}
