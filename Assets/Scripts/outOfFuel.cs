using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outOfFuel : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject noFuelMenuUI;

    private RocketProps rocketProps;
    private RocketSpawner rs;

    private bool outOfFuelBefore = false;

    private TimeKeeper timeKeeper;

    private void Start()
    {
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
        timeKeeper = GameObject.FindObjectOfType<TimeKeeper>();
    }


    // Update is called once per frame
    void Update()
    {

        if (rocketProps == null)
        {
            if (rs != null)
            {
                if (rs.SpawnedRocket != null)
                {
                    rocketProps = rs.SpawnedRocket.GetComponent<RocketProps>();
                }
            }
            else
            {
                rocketProps = GameObject.FindObjectOfType<RocketProps>();
            }
        }
        else
        {
            if (rocketProps.OutOfFuel && outOfFuelBefore == false)
            {
                Cursor.visible = true;
                outOfFuelBefore = true;
                noFuelMenuUI.SetActive(true);
                //Time.timeScale = 0f;
                //GameIsPaused = true;
            }
            else if (outOfFuelBefore && rocketProps.OutOfFuel == false)
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
                outOfFuelBefore = false;
                noFuelMenuUI.SetActive(false);
            }
        }


        /*if (Input.GetKeyDown(KeyCode.Tab)) // FUEL OUT?
        {
            noFuelMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }*/



    }



    public void Retry()
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
            Statics.resetMultiplier += timeKeeper.GetCurrentTime() * 0.04f;
        }
        noFuelMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void loadMenu()
    {
        noFuelMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu_3");

    }

    
}