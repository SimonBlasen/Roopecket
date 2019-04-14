using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLogo: MonoBehaviour {


    public bool Entered = false;
    private bool MenuGo = false;
    

    

    public float seconds = 10;
    public float timer;
    public Vector3 Point;
    public Vector3 Difference;
    public Vector3 start;
    public float percent;



    void Start()
    {
        start = transform.position;
        Point = new Vector3(5, 5, 5);
        Difference = Point - start;
    }

    void Update()
    { 
        if (Entered)
        {

            

            if (tag == "Logo")
            {
                if (timer <= seconds)
                {

                    timer += Time.deltaTime;

                    percent = timer / seconds;


                   
                    transform.position = start + Difference * percent;
                }
            }

        /*    if (tag == "MenuPoints")
            {

                print("Step 2");
                menuText.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                MenuGo = true;
                

            }

        */
        }
    }

    private void OnMouseEnter()
    {
        print("Step 1");
        Entered = true;

    }

}


