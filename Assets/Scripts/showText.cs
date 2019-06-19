﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showText : MonoBehaviour
{
    public string buttonType;
    public Button yourButton;
    public GameObject Text1, Text2, Text3, Text4, Text5;
    private bool isThereText;
    public int rocketN;
  
    private bool informationShwon = false;

    void Start()
    {
        
        isThereText = false;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        
       if (Input.GetKeyDown(KeyCode.Delete) && isThereText)
            {
            closeText();
            }


    }

    void TaskOnClick()
    {
        GameObject Rocket = GameObject.Find("SCRIPTPALLET");
        ArrowRocketSelector currentRocket = Rocket.GetComponent<ArrowRocketSelector>();
        rocketN = currentRocket.rocketNubmber;

        if (buttonType == "information" && isThereText == false)
        {
            isThereText = true;

            if (rocketN == 0)
            {
                Text1.SetActive(true);
                Time.timeScale = 0;
            }

           else if (rocketN == 1)
            {
                Text2.SetActive(true);
                Time.timeScale = 0;
            }

            else if (rocketN == 2)
            {
                Text3.SetActive(true);
                Time.timeScale = 0;
            }

            else if (rocketN == 3)
            {
                Text4.SetActive(true);
                Time.timeScale = 0;
            }

            else if (rocketN == 4)
            {
                Text5.SetActive(true);
                Time.timeScale = 0;
            }

        }

        else if (isThereText)
        {
            closeText();
        }
    }

    void closeText()
    {

        Time.timeScale = 1;
        isThereText = false;
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(false);
        Text4.SetActive(false);
        Text5.SetActive(false);

    }

}
