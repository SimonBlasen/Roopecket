﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flyingGoal : MonoBehaviour {

    public string levelToLoad;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rocket")
        {

            SceneManager.LoadScene(levelToLoad);

        }
    }
}
