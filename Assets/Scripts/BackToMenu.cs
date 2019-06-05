using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

    private Vector3 spawn;
    public Vector3 selectedText;
    private float lerpSpeed = 0.08f;

    private bool click = false;
    private bool mouseHover = false;


    // Use this for initialization
    void Start () {
        spawn = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (mouseHover)
        {

            transform.position = Vector3.Lerp(transform.position, selectedText, lerpSpeed);

            if (click)
            {

                SceneManager.LoadScene("Main_Menu_3");

            }

        }

        else
        {

            transform.position = Vector3.Lerp(transform.position, spawn, lerpSpeed);

        }

	}

    private void OnMouseDown()
    {
        click = true;
    }

    private void OnMouseEnter()
    {
        mouseHover = true;
    }

    private void OnMouseExit()
    {
        mouseHover = false;
    }
}
