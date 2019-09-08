using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocketDestroyed : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject noLifeMenuUI;

    private bool wasDestroyed = false;

    private RocketProps rocketProps;
    private RocketSpawner rs;

    private float timerWaitToShowUI = 2f;

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
            if (wasDestroyed && timerWaitToShowUI > 0f)
            {
                timerWaitToShowUI -= Time.deltaTime;
                if (timerWaitToShowUI <= 0f)
                {
                    Cursor.visible = true;
                    noLifeMenuUI.SetActive(true);
                    //Time.timeScale = 0f;
                    //GameIsPaused = true;
                }
            }

            if (wasDestroyed != rocketProps.IsDestroyed)
            {
                wasDestroyed = rocketProps.IsDestroyed;

            }
        }




    }

    

    public void Retry()
    {
        Cursor.visible = false;

        Statics.resetMultiplier += timeKeeper.GetCurrentTime() * 0.04f;
        noLifeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void loadMenu()
    {
        noLifeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu_3");

    }

}
