using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outOfFuel : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject noFuelMenuUI;






    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Tab)) // FUEL OUT?
        {
            noFuelMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }



    }



    public void Retry()
    {

        noFuelMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Main_Menu_3");

    }

    public void loadMenu()
    {
        noFuelMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu_3");

    }

    
}