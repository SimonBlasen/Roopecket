using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvPRoundWatcher : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textRounds;
    [SerializeField]
    private TextMeshProUGUI textP1Score;
    [SerializeField]
    private TextMeshProUGUI textP2Score;
    [SerializeField]
    private PvPWonTextAnim gameEndedUI;


    private RocketProps rocketProps;
    private RocketProps rocketPropsP2;
    private RocketSpawner rs;

    private bool outOfFuelBefore1 = false;
    private bool outOfFuelBefore2 = false;
    private bool wasDestroyed1 = false;
    private bool wasDestroyed2 = false;

    private int whoWon = 0;

    private float timerWaitToShowUI = 2f;
    private float timerWaitToShowUI2 = 2f;

    // Start is called before the first frame update
    void Start()
    {
        textRounds.text = "Round " + (Statics.pvpRound + 1).ToString();
        textP1Score.text = Statics.pvpScoreP1.ToString();
        textP2Score.text = Statics.pvpScoreP2.ToString();
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
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
                    rocketPropsP2 = rs.SpawnedRocket2.GetComponent<RocketProps>();
                }
            }
        }
        else if (whoWon == 0)
        {
            if (rocketProps.OutOfFuel && outOfFuelBefore1 == false)
            {
                outOfFuelBefore1 = true;
                whoWon = 2;
            }

            if (rocketPropsP2.OutOfFuel && outOfFuelBefore2 == false)
            {
                outOfFuelBefore2 = true;
                whoWon = 1;
            }



            if (wasDestroyed1 && timerWaitToShowUI > 0f)
            {
                timerWaitToShowUI -= Time.deltaTime;
                if (timerWaitToShowUI <= 0f)
                {
                    whoWon = 2;
                }
            }

            if (wasDestroyed1 != rocketProps.IsDestroyed)
            {
                wasDestroyed1 = rocketProps.IsDestroyed;
            }








            if (wasDestroyed2 && timerWaitToShowUI2 > 0f)
            {
                timerWaitToShowUI2 -= Time.deltaTime;
                if (timerWaitToShowUI2 <= 0f)
                {
                    whoWon = 1;
                }
            }

            if (wasDestroyed2 != rocketPropsP2.IsDestroyed)
            {
                wasDestroyed2 = rocketPropsP2.IsDestroyed;
            }
        }

        if (whoWon == 1 || whoWon == 2)
        {
            gameEndedUI.StartAnim(whoWon);

            Statics.pvpRound++;
            if (whoWon == 1)
            {
                Statics.pvpScoreP1++;
            }
            else
            {
                Statics.pvpScoreP2++;
            }
            whoWon = -1;
        }


        if (whoWon == -1)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("LMovers1") || Input.GetButtonDown("LMovers2"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
