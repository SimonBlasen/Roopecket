using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageArrows : MonoBehaviour {

    public bool mouseHover = false;
    public bool mousedown = false;
    private bool locked = false;

    public float left = 0;
    public float right = 0;
    public float rocketNubmber = 1;

    Vector3 spawn;
    Vector3 selected;

    private void Start()
    {

        spawn = transform.position;
        selected = spawn + new Vector3(0f, 0f, -0.2f);

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

        if(mousedown)
        {

            if(right == 1)
            {
                rocketNubmber += 1;
               
                
            }

            if (left == 1)
            {
                rocketNubmber -= 1;
                
            }

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
