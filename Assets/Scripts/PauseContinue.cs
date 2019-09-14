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



    private void Start()
    {
        GameObject instChalWatcher = new GameObject("Challenges Watcher");
        instChalWatcher.AddComponent<ChallengesWatcher>();

        instChalWatcher.GetComponent<ChallengesWatcher>().textChallengeGUI = textChallenge;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
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
        Cursor.visible = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.visible = false;
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

        Application.Quit();

    }
}