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

    // Start is called before the first frame update
    void Start()
    {
        if (Statics.isInFreestyle == false)
        {
            if (SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp] != -1)
            {
                curChallenge = SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp];
            }

            curLevel = GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp;
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
            }
            else
            {
                textChallenge.text = "Challenge: " + SavedGame.GetChallengeName(curChallenge) + "\n  Failed!";
            }
        }
        else
        {
            textChallenge.text = "";
        }



        textChallengeRew.text = "";
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
        }
    }
}
