using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

    public Button yourButton;
    public string LevelToLoad;
    private bool informationShwon = false;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SavedGame.SaveSavegame();
        SceneManager.LoadScene(LevelToLoad);
    }


}
