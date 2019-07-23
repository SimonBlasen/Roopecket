using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageArrows : MonoBehaviour {

    public ArrowRocketSelector ars;
    public bool mouseHover = false;
    public bool mousedown = false;
    private bool locked = false;
    private float lerpSpeed = 0.05f;
    public bool controlableWithKeyboard = true;

    public float left = 0;
    public float right = 0;
    public int rocketNubmber = 1;

    public Transform[] rockets;
    public Transform currentRocket;

    public Vector3 selectedOffset = Vector3.zero;
    Vector3 spawn;
    Vector3 selected;

    public Vector3 rocketSpawn;

    private bool mouseIsDown = false;

    private void Start()
    {

        spawn = transform.position;
        if (selectedOffset == Vector3.zero)
        {
            selected = spawn + new Vector3(0f, 0f, -0.2f);
        }
        else
        {
            selected = spawn + selectedOffset;
        }

        //for (int i = 0; i < rockets.Length; i++)
        //{
        //    rockets[i].gameObject.SetActive(false);
        //}

        //currentRocket = rockets[0];
        //currentRocket.gameObject.SetActive(true);
        //currentRocket.transform.position = rocketSpawn;
        //currentRocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //currentRocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void Update () {


        if (mouseHover)
        {

            transform.position = Vector3.Lerp(transform.position, selected, lerpSpeed);

        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, spawn, lerpSpeed);
        }

        if(mousedown && mouseIsDown == false && KeysLocked == false)
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

        else if (((Input.GetKeyDown(KeyCode.RightArrow) && mouseIsDown == false && controlableWithKeyboard) || (Input.GetKeyDown(KeyCode.D) && mouseIsDown == false && controlableWithKeyboard)) && KeysLocked == false)
        {
            ars.RightClick();

        }

        else if (((Input.GetKeyDown(KeyCode.LeftArrow) && mouseIsDown == false && controlableWithKeyboard) || (Input.GetKeyDown(KeyCode.A) && mouseIsDown == false && controlableWithKeyboard)) && KeysLocked == false)
        {

            ars.LeftClick();

        }





        else if (mousedown == false && mouseIsDown)
        {
            mouseIsDown = false;
         
        }







	}

    public bool KeysLocked
    {
        get;set;
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
