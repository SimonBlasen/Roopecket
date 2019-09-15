using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengesWatcher : MonoBehaviour
{
    private string chalComplText = "Challenge\nCompleted!";
    public TextMeshProUGUI textChallengeGUI;

    private bool challengeCompl = false;

    private int curChallenge = -1;
    private int somersaults = 0;

    private int curLevel = 0;

    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
        if (Statics.isInFreestyle == false)
        {
            if (SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp] != -1)
            {
                curChallenge = SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp];
            }

            curLevel = GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp;

            if (curChallenge == 3)
            {
                textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  " + (somersaults).ToString("n0") + " / " + SavedGame.GetChallengeValue(curLevel, curChallenge).ToString("n0");
            }
        }
        else
        {
            curChallenge = -1;
            textChallengeGUI.text = "";
        }
    }
    TimeKeeper tk = null;

    // Update is called once per frame
    void Update()
    {
        if (tk == null)
        {
            tk = GameObject.FindObjectOfType<TimeKeeper>();
        }
        else
        {
            if (curChallenge == 1)
            {
                if (tk.GetCurrentTime() > SavedGame.GetChallengeValue(curLevel, curChallenge))
                {
                    challengeCompl = false;
                    textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  Failed!";
                }
                else
                {
                    textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  " + (SavedGame.GetChallengeValue(curLevel, curChallenge) - tk.GetCurrentTime()).ToString("n2");
                }

            }
            else if (curChallenge == 0)
            {
                if (StaticsSingleplayer.ReadTakenDamage() > SavedGame.GetChallengeValue(curLevel, curChallenge))
                {
                    challengeCompl = false;
                    textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  Failed!";
                }
            }
            else if (curChallenge == 2)
            {
                if (StaticsSingleplayer.ReadFuelUsed() > SavedGame.GetChallengeValue(curLevel, curChallenge))
                {
                    challengeCompl = false;
                    textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  Failed!";
                }
                else
                {
                    textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  " + (StaticsSingleplayer.ReadFuelUsed()).ToString("n2") + " / " + SavedGame.GetChallengeValue(curLevel, curChallenge).ToString("n0");
                }
            }
        }
    }

    public void FinishedLevel(TextMeshProUGUI textChallenge, TextMeshProUGUI textChallengeRew, float time, float fuel, float damage)
    {
        if (curChallenge == 0)
        {
            if (damage <= SavedGame.GetChallengeValue(curLevel, curChallenge))
            {
                challengeCompl = true;
            }
        }
        else if (curChallenge == 1)
        {
            if (time <= SavedGame.GetChallengeValue(curLevel, curChallenge))
            {
                challengeCompl = true;
            }
        }
        else if (curChallenge == 2)
        {
            if (fuel <= SavedGame.GetChallengeValue(curLevel, curChallenge))
            {
                challengeCompl = true;
            }
        }

        if (curChallenge != -1)
        {
            if (challengeCompl)
            {
                textChallenge.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  Completed!";

                int reward = SavedGame.GetChallengeReward();

                if (reward == 0 && curLevel != 19)
                {
                    textChallengeRew.text = "Rocket key in\nnext level";

                    SavedGame.ChallengeRewards[SavedGame.LastPlayedRocket, curLevel + 1] = -2;
                }
                else if (reward == 1)
                {
                    int rw = Random.Range(1, 8);
                    rw *= 100;

                    textChallengeRew.text = "Additional rocketworth\nof " + rw.ToString();

                    SavedGame.ChallengeRewards[SavedGame.LastPlayedRocket, curLevel] = rw;
                }

            }
            else
            {
                textChallenge.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  Failed!";
                textChallengeRew.text = "";
            }





        }
        else
        {
            textChallenge.text = "";
            textChallengeRew.text = "";
        }



    }

    public void FlewSomersault()
    {
        if (curChallenge == 3)
        {
            somersaults++;

            if (somersaults >= SavedGame.GetChallengeValue(curLevel, curChallenge))
            {
                challengeCompl = true;

                textChallengeGUI.text = chalComplText;
            }
            else
            {
                textChallengeGUI.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  " + (somersaults).ToString("n0") + " / " + SavedGame.GetChallengeValue(curLevel, curChallenge).ToString("n0");
            }
        }
    }
}
