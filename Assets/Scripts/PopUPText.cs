using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUPText : MonoBehaviour
{

    public string buttonType;
    public Button yourButton;
    public GameObject Text1;
  

    void Start()
    {

        Time.timeScale = 1f;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        if (SavedGame.Money != 500 || SavedGame.Money != 0)
        {
            TaskOnClick();
        }
    }



    void TaskOnClick()
    {

        Text1.SetActive(false);
        Time.timeScale = 1f;

    }
}
