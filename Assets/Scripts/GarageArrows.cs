﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageArrows : MonoBehaviour {

    public ArrowRocketSelector ars;
    public bool mouseHover = false;
    public bool mousedown = false;
    private bool locked = false;

    public float left = 0;
    public float right = 0;
    public int rocketNubmber = 1;

    public Transform[] rockets;
    public Transform currentRocket;

    Vector3 spawn;
    Vector3 selected;

    public Vector3 rocketSpawn;

    private bool mouseIsDown = false;

    private void Start()
    {

        spawn = transform.position;
        selected = spawn + new Vector3(0f, 0f, -0.2f);

        for (int i = 0; i < rockets.Length; i++)
        {
            rockets[i].gameObject.SetActive(false);
        }

        currentRocket = rockets[0];
        currentRocket.gameObject.SetActive(true);
        currentRocket.transform.position = rocketSpawn;
        currentRocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentRocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void Update () {


        if (mouseHover)
        {

            transform.position = selected;

        }

        else
        {
            transform.position = spawn;
        }

        if(mousedown && mouseIsDown == false)
        {
            mouseIsDown = true;

            if(right == 1)
            {
                ars.RightClick();
            }

            if (left == 1)
            {
                ars.LeftClick();

            }
            


        }
        else if (mousedown == false && mouseIsDown)
        {
            mouseIsDown = false;
         
        }







	}


    private void OnMouseEnter()
    {
        mouseHover = true;
    }

    private void OnMouseExit()
    {
        mouseHover = false;
    }

    private void OnMouseDown()
    {
       
            mousedown = true;
         
    }

    private void OnMouseUp()
    {
        mousedown = false;
    }
}
