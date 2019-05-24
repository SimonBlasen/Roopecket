using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocketDestroyed : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject noLifeMenuUI;






    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.Tab)) // FUEL OUT?
        {
            noLifeMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }



    }

    

    public void Retry()
    {

        noLifeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Main_Menu_3");

    }

    public void loadMenu()
    {
        noLifeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu_3");

    }

}
