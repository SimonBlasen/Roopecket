using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseContinue : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI textChallenge;
    private float mousex, mousey;

    public AudioClip[] challengeClips;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("Tutorial"))
        {
            textChallenge.text = "";
        }
        GameObject instChalWatcher = new GameObject("Challenges Watcher");
        instChalWatcher.AddComponent<ChallengesWatcher>();

        instChalWatcher.GetComponent<ChallengesWatcher>().textChallengeGUI = textChallenge;
        instChalWatcher.GetComponent<ChallengesWatcher>().audioClip = challengeClips[SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp]];

        if (SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp] != -1)
        {
            textChallenge.text = "Challenge\n  " + SavedGame.GetChallengeName(SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp]);
        }
        else
        {
            textChallenge.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }

            mousex = Input.mousePosition.x;
            mousex = Input.mousePosition.y;

          if (Cursor.visible == false && mousex != Input.mousePosition.x && mousey != Input.mousePosition.y)
            {

                Cursor.visible = true;
                float timer = 3f;  
                if(timer >= 0f)
                {

                    timer -= Time.deltaTime;

                }
                Cursor.visible = false;
            }






        }

    }


        






    public void PauseGame()
    {
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void Resume()
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

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

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
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        if (Statics.testingGarageRocket == false)
        {
            SceneManager.LoadScene("Main_Menu_3");
        }
        else
        {
            Statics.testingGarageRocket = false;
            SceneManager.LoadScene("Garage");
        }


    }

    public void quitGame()
    {
        SavedGame.SaveSavegame();

        Application.Quit();

    }
}