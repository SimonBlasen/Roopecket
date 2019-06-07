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


    private void Start()
    {
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
    }


    // Update is called once per frame
    void Update()
    {

        if (rocketProps == null)
        {
            if (rs.SpawnedRocket != null)
            {
                rocketProps = rs.SpawnedRocket.GetComponent<RocketProps>();
            }
        }
        else
        {
            if (rocketProps.OutOfFuel && outOfFuelBefore == false)
            {
                outOfFuelBefore = true;
                noFuelMenuUI.SetActive(true);
                //Time.timeScale = 0f;
                //GameIsPaused = true;
            }
            else if (outOfFuelBefore && rocketProps.OutOfFuel == false)
            {
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