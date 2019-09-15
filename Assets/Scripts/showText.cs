using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    }

    private void Update()
    {
        
       if (Input.GetKeyDown(KeyCode.Delete) && isThereText)
            {
            closeText();
            }


    }

    public void TaskOnClick()
    {
        //GameObject Rocket = GameObject.Find("SCRIPTPALLET");
        //ArrowRocketSelector currentRocket = Rocket.GetComponent<ArrowRocketSelector>();
        rocketN = Statics.selectedRocket;

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
        if (SceneManager.GetActiveScene().name != "Tutorial1"
                  && SceneManager.GetActiveScene().name != "Tutorial1.1"
                  && SceneManager.GetActiveScene().name != "Tutorial1.2"
                  && SceneManager.GetActiveScene().name != "Tutorial2"
                  && SceneManager.GetActiveScene().name != "Tutorial2.1"
                  && SceneManager.GetActiveScene().name != "Tutorial3"
                  && SceneManager.GetActiveScene().name != "Tutorial4"
                  && SceneManager.GetActiveScene().name != "Tutorial5")
        {
            Cursor.visible = false;
        }

        Time.timeScale = 1;
        isThereText = false;
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(false);
        Text4.SetActive(false);
        Text5.SetActive(false);

    }

}
