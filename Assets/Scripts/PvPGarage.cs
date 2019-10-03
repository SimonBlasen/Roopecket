using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PvPGarageState
{
    CONTROLS_P1, CONTROLS_P2, ROCKETS_SELECT
}

public class PvPGarage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] rocketsRight;
    [SerializeField]
    private GameObject[] rocketsLeft;
    [SerializeField]
    private GameObject textP1Controls;
    [SerializeField]
    private GameObject textP2Controls;
    [SerializeField]
    private GameObject panelButtonStart;


    private PvPGarageState state = PvPGarageState.CONTROLS_P1;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rocketsRight.Length; i++)
        {
            rocketsRight[i].SetActive(false);
        }
        for (int i = 0; i < rocketsLeft.Length; i++)
        {
            rocketsLeft[i].SetActive(false);
        }

        panelButtonStart.SetActive(false);
        textP1Controls.GetComponent<TextMeshProUGUI>().enabled = true;
        textP2Controls.GetComponent<TextMeshProUGUI>().enabled = false;

        Statics.deviceP1 = -1;
        Statics.deviceP2 = -1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PvPGarageState.CONTROLS_P1:
                for (int i = 0; i < 20; i++)
                {
                    if (Input.GetKeyDown("joystick 1 button " + i))
                    {
                        Statics.deviceP1 = 1;
                        state = PvPGarageState.CONTROLS_P2;
                        textP1Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                        textP2Controls.GetComponent<TextMeshProUGUI>().enabled = true;
                        break;
                    }
                }
                for (int i = 0; i < 20; i++)
                {
                    if (Input.GetKeyDown("joystick 2 button " + i))
                    {
                        Statics.deviceP1 = 2;
                        state = PvPGarageState.CONTROLS_P2;
                        textP1Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                        textP2Controls.GetComponent<TextMeshProUGUI>().enabled = true;
                        break;
                    }
                }

                if (isAnyKeyboardKeyDown())
                {
                    Statics.deviceP1 = 0;
                    state = PvPGarageState.CONTROLS_P2;
                    textP1Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                    textP2Controls.GetComponent<TextMeshProUGUI>().enabled = true;
                }

                break;
            case PvPGarageState.CONTROLS_P2:
                if (Statics.deviceP1 != 1)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (Input.GetKeyDown("joystick 1 button " + i))
                        {
                            Statics.deviceP2 = 1;
                            state = PvPGarageState.ROCKETS_SELECT;
                            panelButtonStart.SetActive(true);
                            setActiveRocket(true, true);
                            setActiveRocket(false, true);
                            textP1Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                            textP2Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                            break;
                        }
                    }
                }

                if (Statics.deviceP1 != 2)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (Input.GetKeyDown("joystick 2 button " + i))
                        {
                            Statics.deviceP2 = 2;
                            state = PvPGarageState.ROCKETS_SELECT;
                            panelButtonStart.SetActive(true);
                            setActiveRocket(true, true);
                            setActiveRocket(false, true);
                            textP1Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                            textP2Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                            break;
                        }
                    }
                }

                if (Statics.deviceP1 != 0)
                {
                    if (isAnyKeyboardKeyDown())
                    {
                        Statics.deviceP2 = 0;
                        state = PvPGarageState.ROCKETS_SELECT;
                        panelButtonStart.SetActive(true);
                        setActiveRocket(true, true);
                        setActiveRocket(false, true);
                        textP1Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                        textP2Controls.GetComponent<TextMeshProUGUI>().enabled = false;
                    }
                }

                break;
            case PvPGarageState.ROCKETS_SELECT:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (Statics.deviceP1 == 0)
                    {
                        setActiveRocket(true, false);
                    }
                    if (Statics.deviceP2 == 0)
                    {
                        setActiveRocket(false, false);
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (Statics.deviceP1 == 0)
                    {
                        setActiveRocket(true, true);
                    }
                    if (Statics.deviceP2 == 0)
                    {
                        setActiveRocket(false, true);
                    }
                }

                if (Input.GetAxis("Controller1X") < -0.5f && oldCon1X >= -0.5f)
                {
                    if (Statics.deviceP1 == 1)
                    {
                        setActiveRocket(true, false);
                    }
                    if (Statics.deviceP2 == 1)
                    {
                        setActiveRocket(false, false);
                    }
                }
                if (Input.GetAxis("Controller1X") > 0.5f && oldCon1X <= 0.5f)
                {
                    if (Statics.deviceP1 == 1)
                    {
                        setActiveRocket(true, true);
                    }
                    if (Statics.deviceP2 == 1)
                    {
                        setActiveRocket(false, true);
                    }
                }

                if (Input.GetAxis("Controller2X") < -0.5f && oldCon2X >= -0.5f)
                {
                    if (Statics.deviceP1 == 2)
                    {
                        setActiveRocket(true, false);
                    }
                    if (Statics.deviceP2 == 2)
                    {
                        setActiveRocket(false, false);
                    }
                }
                if (Input.GetAxis("Controller2X") > 0.5f && oldCon2X <= 0.5f)
                {
                    if (Statics.deviceP1 == 2)
                    {
                        setActiveRocket(true, true);
                    }
                    if (Statics.deviceP2 == 2)
                    {
                        setActiveRocket(false, true);
                    }
                }


                oldCon1X = Input.GetAxis("Controller1X");
                oldCon2X = Input.GetAxis("Controller2X");

                break;
        }
    }

    private float oldCon1X = 0f;
    private float oldCon2X = 0f;

    private int rocketSelIndexP1 = -1;
    private int rocketSelIndexP2 = -1;

    private void setActiveRocket(bool player1, bool forward)
    {
        int rocketType = -1;
        if (player1)
        {
            rocketSelIndexP1 = manipIndex(rocketSelIndexP1, forward);
            rocketType = SavedGame.OwnedRockets[rocketSelIndexP1];

            for (int i = 0; i < rocketsLeft.Length; i++)
            {
                rocketsLeft[i].SetActive(i == rocketType);
            }

            Statics.selectedRocket = rocketType;
        }
        else
        {
            rocketSelIndexP2 = manipIndex(rocketSelIndexP2, forward);
            rocketType = SavedGame.OwnedRockets[rocketSelIndexP2];

            for (int i = 0; i < rocketsRight.Length; i++)
            {
                rocketsRight[i].SetActive(i == rocketType);
            }

            Statics.selectedRocketP2 = rocketType;
        }
    }

    private int manipIndex(int oldIndex, bool forward)
    {
        int oldold = oldIndex;

        if (forward)
        {
            oldIndex++;
        }
        else
        {
            oldIndex--;
        }
        if (oldIndex < 0)
        {
            oldIndex = SavedGame.OwnedRockets.Length - 1;
        }
        else if (oldIndex >= SavedGame.OwnedRockets.Length)
        {
            oldIndex = 0;
        }

        int counter = 0;
        while (SavedGame.OwnedRockets[oldIndex] == -1)
        {
            counter++;
            if (counter > 300)
            {
                Debug.LogError("Broke out of while. Means, you don't own a rocket atm");
                break;
            }
            if (forward)
            {
                oldIndex++;
            }
            else
            {
                oldIndex--;
            }
            if (oldIndex < 0)
            {
                oldIndex = SavedGame.OwnedRockets.Length - 1;
            }
            else if (oldIndex >= SavedGame.OwnedRockets.Length)
            {
                oldIndex = 0;
            }
        }
        Debug.Log("Old: " + oldold + ", New: " + oldIndex);
        return oldIndex;
    }

    public static bool isAnyKeyboardKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.B) ||
            Input.GetKeyDown(KeyCode.Q) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.R) ||
            Input.GetKeyDown(KeyCode.T) ||
            Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.U) ||
            Input.GetKeyDown(KeyCode.I) ||
            Input.GetKeyDown(KeyCode.O) ||
            Input.GetKeyDown(KeyCode.P) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.F) ||
            Input.GetKeyDown(KeyCode.G) ||
            Input.GetKeyDown(KeyCode.H) ||
            Input.GetKeyDown(KeyCode.J) ||
            Input.GetKeyDown(KeyCode.K) ||
            Input.GetKeyDown(KeyCode.L) ||
            Input.GetKeyDown(KeyCode.Y) ||
            Input.GetKeyDown(KeyCode.X) ||
            Input.GetKeyDown(KeyCode.C) ||
            Input.GetKeyDown(KeyCode.V) ||
            Input.GetKeyDown(KeyCode.N) ||
            Input.GetKeyDown(KeyCode.M))
        {
            return true;
        }
        return false;
    }

    public void ButtonStartClick()
    {
        Statics.pvpRound = 0;
        Statics.pvpScoreP1 = 0;
        Statics.pvpScoreP2 = 0;
        Statics.isSplitscreen = true;
        SceneManager.LoadScene("Platform PvP Simon 2");
    }
}
