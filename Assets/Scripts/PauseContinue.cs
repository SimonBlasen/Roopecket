using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseContinue : MonoBehaviour {

   

    private bool showPauseScreen = false;
    private bool menuGO = false;

    public int WSH = 20, WSW = 20;

    public float startTime;
    private string currentTime;
    public Rect timerRect;



	// Use this for initialization
	void Start () {

        timerRect = new Rect(10, 10, 400, 100);

	}
	
	// Update is called once per frame
	void Update () {
		
        if(menuGO)
        {
            showPauseScreen = true;
        }

        startTime += Time.deltaTime;
        currentTime = string.Format("{0:0.0}",startTime);


        

	}

    private void OnGUI()
    {

        if (showPauseScreen)
        {
            print("Pause");
            Rect winScreenRect = new Rect(Screen.width / 2 - (WSW / 2), Screen.height / 2 - (WSH / 2), WSW, WSH);

            Time.timeScale = 0;

            GUI.Box(winScreenRect, "PAUSE");

            if (GUI.Button(new Rect((winScreenRect.x + winScreenRect.width - 170), (winScreenRect.y + winScreenRect.height - 60), 150, 40), "Main Menu"))
            {
                SceneManager.LoadScene("Main_Menu_3");
            }
        }

        GUI.Label(timerRect, currentTime);
    }

    private void OnMouseDown()
    {
        menuGO = true;
    }
}
