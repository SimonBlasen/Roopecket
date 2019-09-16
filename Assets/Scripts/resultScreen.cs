using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum EndscreenState
{
    START = 255, STAGE_TIME = 0, STAGE_DAMAGE = 1, STAGE_FUEL = 2, STAGE_WORTH = 3, WAIT_STAGE_TO_GLOBAL = 4, GLOBAL_TIME = 5, GLOBAL_DAMAGE = 6, GLOBAL_FUEL = 7, GLOBAL_WORTH = 8, MONEY = 9, 
    WAIT_STAGE_TIME = 257, WAIT_STAGE_DAMAGE = 258, WAIT_STAGE_FUEL = 259, WAIT_STAGE_WORTH = 260, WAIT_GLOBAL_TIME = 261, WAIT_GLOBAL_DAMAGE = 262, WAIT_GLOBAL_FUEL = 263, WAIT_GLOBAL_WORTH = 264, WAIT_MONEY = 265,
    END = 256
}

public static class EndscreenStateMethods
{
    public static float Time(this EndscreenState state)
    {
        float textTime = 1.5f;
        float textAddTime = 1f;

        float textWaitsBetween = 0.3f;

        switch (state)
        {
            case EndscreenState.START:
                return 0f;
            case EndscreenState.STAGE_TIME:
                return textTime;
            case EndscreenState.STAGE_DAMAGE:
                return textTime;
            case EndscreenState.STAGE_FUEL:
                return textTime;
            case EndscreenState.STAGE_WORTH:
                return textTime;
            case EndscreenState.WAIT_STAGE_TO_GLOBAL:
                return 2.5f;
            case EndscreenState.GLOBAL_TIME:
                return textAddTime;
            case EndscreenState.GLOBAL_DAMAGE:
                return textAddTime;
            case EndscreenState.GLOBAL_FUEL:
                return textAddTime;
            case EndscreenState.GLOBAL_WORTH:
                return textAddTime;
            case EndscreenState.MONEY:
                return textAddTime;
            case EndscreenState.WAIT_STAGE_TIME:
                return textWaitsBetween;
            case EndscreenState.WAIT_STAGE_DAMAGE:
                return textWaitsBetween;
            case EndscreenState.WAIT_STAGE_FUEL:
                return textWaitsBetween;
            case EndscreenState.WAIT_STAGE_WORTH:
                return textWaitsBetween;
            case EndscreenState.WAIT_GLOBAL_TIME:
                return textWaitsBetween;
            case EndscreenState.WAIT_GLOBAL_DAMAGE:
                return textWaitsBetween;
            case EndscreenState.WAIT_GLOBAL_FUEL:
                return textWaitsBetween;
            case EndscreenState.WAIT_GLOBAL_WORTH:
                return textWaitsBetween;
            case EndscreenState.WAIT_MONEY:
                return 1.5f;
            default:
                return 1000f;
        }
    }

    public static EndscreenState NextState(this EndscreenState state)
    {
        //Debug.Log("State was: " + state.ToString());
        switch (state)
        {
            case EndscreenState.START:
                return EndscreenState.STAGE_TIME;
            case EndscreenState.STAGE_TIME:
                return EndscreenState.WAIT_STAGE_DAMAGE;
            case EndscreenState.WAIT_STAGE_DAMAGE:
                return EndscreenState.STAGE_DAMAGE;
            case EndscreenState.STAGE_DAMAGE:
                return EndscreenState.WAIT_STAGE_FUEL;
            case EndscreenState.WAIT_STAGE_FUEL:
                return EndscreenState.STAGE_FUEL;
            case EndscreenState.STAGE_FUEL:
                return EndscreenState.WAIT_STAGE_WORTH;
            case EndscreenState.WAIT_STAGE_WORTH:
                return EndscreenState.STAGE_WORTH;
            case EndscreenState.STAGE_WORTH:
                return EndscreenState.WAIT_STAGE_TO_GLOBAL;
            case EndscreenState.WAIT_STAGE_TO_GLOBAL:
                return EndscreenState.GLOBAL_TIME;
            case EndscreenState.GLOBAL_TIME:
                return EndscreenState.WAIT_GLOBAL_DAMAGE;
            case EndscreenState.WAIT_GLOBAL_DAMAGE:
                return EndscreenState.GLOBAL_DAMAGE;
            case EndscreenState.GLOBAL_DAMAGE:
                return EndscreenState.WAIT_GLOBAL_FUEL;
            case EndscreenState.WAIT_GLOBAL_FUEL:
                return EndscreenState.GLOBAL_FUEL;
            case EndscreenState.GLOBAL_FUEL:
                return EndscreenState.WAIT_GLOBAL_WORTH;
            case EndscreenState.WAIT_GLOBAL_WORTH:
                return EndscreenState.GLOBAL_WORTH;
            case EndscreenState.WAIT_GLOBAL_TIME:
                return EndscreenState.GLOBAL_TIME;
            case EndscreenState.GLOBAL_WORTH:
                return EndscreenState.WAIT_MONEY;
            case EndscreenState.WAIT_MONEY:
                return EndscreenState.MONEY;
            default:
                return EndscreenState.END;
        }
    }
}

public class resultScreen : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    public TextMeshProUGUI moneyText, moneyTextValue, totalDamageText, DamageText, totalTimeText, timeText, fuelText, totalFuelText, rocketWorth, rocketWorthTotal;
    public TextMeshProUGUI[] textMeshes;
    public TextMeshProUGUI[] textMeshesGlobal;
    public GameObject panelLeaderboard;
    public TextMeshProUGUI textChallenge;
    public TextMeshProUGUI textChallengeReward;
    private string[] t_texts;
    private float[] globalValues;
    private float[] globalValuesOld;
    private string t_damage, t_damageTotal, t_time, t_timeTotal, t_fuel, t_fuelTotal, t_worth, t_worthTotal;
    private float currentRocketWorth;
    private const float damageFactor = 1.5f;
    private const float timeFactor = 0.6f;
    private const float fuelFactor = 1.0f;
    private const float worthFactor = 4000f;

    private int oldMoney = 0;

    private EndscreenState state = EndscreenState.END;
    private float counter = 0f;
    private bool startShowingEndscreen = false;
    private RocketSpawner rs;
    private int maxLifeRocket = -1;
    private bool isSimpleEndscreen = false;

    private void Start()
    {
        for (int i = 0; i < lbName.Length; i++)
        {
            lbName[i].text = "";
            lbPos[i].text = "";
            lbTime[i].text = "";
        }

        t_texts = new string[textMeshes.Length];
        globalValues = new float[textMeshes.Length];
        globalValuesOld = new float[textMeshes.Length];
        GetComponent<Canvas>().enabled = false;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
        if (rs == null)
        {
            maxLifeRocket = GameObject.FindObjectOfType<RocketProps>().MaxHealth;
        }
    }



    private void Update()
    {
        if (maxLifeRocket == -1 && rs != null && rs.SpawnedRocket != null)
        {
            maxLifeRocket = rs.SpawnedRocket.GetComponent<RocketProps>().MaxHealth;
        }
        counter += Time.deltaTime;

        if (state != EndscreenState.END && state != EndscreenState.START)
        {
            if (((int)state) <= 3)
            {
                if (counter > state.Time())
                {
                    textMeshes[(int)state].text = t_texts[(int)state];

                    state = state.NextState();
                    counter = 0f;
                }
                else
                {
                    float perc = counter / state.Time();

                    int letters = (int)(perc * t_texts[(int)state].Length + 0.5f);
                    textMeshes[(int)state].text = t_texts[(int)state].Substring(0, letters);
                }
            }
            else
            {
                if (state == EndscreenState.WAIT_STAGE_TO_GLOBAL)
                {
                    if (counter > state.Time())
                    {
                        state = state.NextState();
                        counter = 0f;
                    }
                }
                else if (state == EndscreenState.GLOBAL_TIME || state == EndscreenState.GLOBAL_DAMAGE || state == EndscreenState.GLOBAL_FUEL || state == EndscreenState.GLOBAL_WORTH)
                {
                    string toStringFormat = "";
                    if (state == EndscreenState.GLOBAL_TIME)
                    {
                        toStringFormat = "n3";
                    }
                    else if (state == EndscreenState.GLOBAL_DAMAGE)
                    {
                        toStringFormat = "n0";
                    }
                    else if (state == EndscreenState.GLOBAL_FUEL)
                    {
                        toStringFormat = "n2";
                    }
                    else if (state == EndscreenState.GLOBAL_WORTH)
                    {
                        toStringFormat = "n2";
                    }


                    int index = ((int)state) - ((int)EndscreenState.GLOBAL_TIME);

                    if (counter > state.Time())
                    {
                        if (index != 1 || isSimpleEndscreen == false)
                        {
                            textMeshesGlobal[index].text = globalValues[index].ToString(toStringFormat);
                        }
                        else
                        {
                            textMeshesGlobal[index].text = globalValues[index].ToString(toStringFormat) + " %";
                        }

                        state = state.NextState();
                        counter = 0f;
                    }
                    else
                    {
                        float perc = counter / state.Time();

                        float val = globalValuesOld[index] + (globalValues[index] - globalValuesOld[index]) * perc;

                        if (index != 1 || isSimpleEndscreen == false)
                        {
                            textMeshesGlobal[index].text = val.ToString(toStringFormat);
                        }
                        else
                        {
                            textMeshesGlobal[index].text = val.ToString(toStringFormat) + " %";
                        }
                    }
                }
                else if (state == EndscreenState.MONEY)
                {
                    if (counter > state.Time())
                    {
                        moneyTextValue.text = SavedGame.Money.ToString();

                        state = state.NextState();
                        counter = 0f;
                    }
                    else
                    {
                        float perc = counter / state.Time();

                        int val = (int)(oldMoney + (SavedGame.Money - oldMoney) * perc);

                        moneyTextValue.text = val.ToString();
                    }
                }

                // Waiting
                else
                {
                    if (counter > state.Time())
                    {
                        state = state.NextState();
                        counter = 0f;
                    }
                }
            }
        }
        else if (state == EndscreenState.START)
        {
            state = state.NextState();
            counter = 0f;
        }


        if (startShowingEndscreen && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            ButtonContinueClick();
        }
    }

    public TextMeshProUGUI[] lbPos;
    public TextMeshProUGUI[] lbTime;
    public TextMeshProUGUI[] lbName;

    public void RefreshLeaderboard()
    {

        LevelLeaderboardDownloader lld = GameObject.FindObjectOfType<LevelLeaderboardDownloader>();

        lbPos[0].text = "1.";
        lbTime[0].text = (lld.timeFirst / 1000f).ToString();
        lbName[0].text = lld.nameFirst;

        for (int i = 0; i < lbPos.Length - 1; i++)
        {
            if (i < lld.places.Length)
            {
                lbPos[i + 1].text = lld.places[i] + ".";
                lbTime[i + 1].text = (lld.times[i] / 1000f).ToString();
                lbName[i + 1].text = lld.names[i];
            }
        }
    }

    public void ShowLeaderboard()
    {
        Debug.Log("Showing leaderboard");
        panelLeaderboard.SetActive(true);

        RefreshLeaderboard();

        
    }

    private void updateLeaderboard()
    {
        float time = SavedGame.CurrentTimeStage[SavedGame.LastPlayedRocket, Statics.currentLevel];

        GameObject.FindObjectOfType<LevelLeaderboardDownloader>().UploadScore((int)(time * 1000f), this);
    }

    public void showEndscreenSimple()
    {
        updateLeaderboard();
        if (GameObject.FindObjectOfType<ChallengesWatcher>() != null)
        {
            GameObject.FindObjectOfType<ChallengesWatcher>().FinishedLevel(textChallenge, textChallengeReward, SavedGame.CurrentTimeStage[SavedGame.LastPlayedRocket, Statics.currentLevel], SavedGame.CurrentUsedFuel[SavedGame.LastPlayedRocket, Statics.currentLevel], SavedGame.CurrentDamageStage[SavedGame.LastPlayedRocket, Statics.currentLevel]);
        }
        Cursor.visible = true;
        Statics.resetMultiplier = 0f;
        isSimpleEndscreen = true;
        startShowingEndscreen = true;
        oldMoney = SavedGame.Money;
        moneyTextValue.text = SavedGame.Money.ToString();

        state = EndscreenState.WAIT_GLOBAL_TIME;

        counter = 0f;

        GetComponent<Canvas>().enabled = true;

        for (int i = 0; i < textMeshes.Length; i++)
        {
            textMeshes[i].text = "";
        }

        globalValuesOld[0] = 0f;
        globalValuesOld[1] = 0f;
        globalValuesOld[2] = 0f;
        globalValuesOld[3] = 0f;
        globalValues[0] = SavedGame.CurrentTimeStage[SavedGame.LastPlayedRocket, Statics.currentLevel];
        globalValues[1] = SavedGame.CurrentDamageStage[SavedGame.LastPlayedRocket, Statics.currentLevel] * 100f / maxLifeRocket;
        globalValues[2] = SavedGame.CurrentUsedFuel[SavedGame.LastPlayedRocket, Statics.currentLevel];
        globalValues[3] = CalculateRocketWorth(globalValues[0], globalValues[1], globalValues[2], 1, new int[] { Statics.currentLevel });
        //globalValues[3] += SavedGame.RocketPrices[Statics.selectedRocket];

        textMeshesGlobal[0].text = globalValuesOld[0].ToString("n3");
        textMeshesGlobal[1].text = globalValuesOld[1].ToString() + " %";
        textMeshesGlobal[2].text = globalValuesOld[2].ToString("n2");
        textMeshesGlobal[3].text = globalValuesOld[3].ToString("n2");

        if (Statics.isInFreestyle)
        {
            levelText.text = "Level " + (Statics.currentLevel + 1).ToString() + " (Freestyle)";
        }
        else
        {
            levelText.text = "Level " + (Statics.currentLevel + 1).ToString();
        }



    }

    public void showEndScreen()
    {
        updateLeaderboard();
        if (GameObject.FindObjectOfType<ChallengesWatcher>() != null)
        {
            GameObject.FindObjectOfType<ChallengesWatcher>().FinishedLevel(textChallenge, textChallengeReward, SavedGame.CurrentTimeStage[SavedGame.LastPlayedRocket, Statics.currentLevel], SavedGame.CurrentUsedFuel[SavedGame.LastPlayedRocket, Statics.currentLevel], SavedGame.CurrentDamageStage[SavedGame.LastPlayedRocket, Statics.currentLevel]);
        }
        Cursor.visible = true;
        Statics.resetMultiplier = 0f;
        isSimpleEndscreen = false;
        startShowingEndscreen = true;
        oldMoney = SavedGame.Money;
        moneyTextValue.text = SavedGame.Money.ToString();

        state = EndscreenState.START;

        counter = 0f;

        GetComponent<Canvas>().enabled = true;

        for (int i = 0; i < textMeshes.Length; i++)
        {
            textMeshes[i].text = "";
        }


        t_texts[0] = SavedGame.CurrentRocketStageTime.ToString("n3");
        t_texts[1] = SavedGame.CurrentRocketStageDamage.ToString();
        t_texts[2] = SavedGame.CurrentRocketStageFuel.ToString("n2");

        List<int> whichLevels = new List<int>();
        float rocketWorthStage = 0f;// CalculateRocketWorth(SavedGame.CurrentRocketStageTime, SavedGame.CurrentRocketStageDamage, SavedGame.CurrentRocketStageFuel, 1, whichLevels.ToArray());
        for (int i = (LevelNumber.GetFirstLevelOfStage(LevelNumber.GetStage(Statics.currentLevel))); i <= Statics.currentLevel; i++)
        {
            whichLevels.Add(i);
            //rocketWorthStage += CalculateRocketWorth(SavedGame.CurrentRocketStageTime, SavedGame.CurrentRocketStageDamage, SavedGame.CurrentRocketStageFuel, 1, new int[] { i });
        }
        rocketWorthStage = SavedGame.GetGlobalWorthStage(SavedGame.LastPlayedRocket, LevelNumber.GetStage(Statics.currentLevel));
        //rocketWorthStage += SavedGame.RocketPrices[Statics.selectedRocket];


        //float rocketWorthStage = worthFaktor / (SavedGame.CurrentRocketGlobalTime * (SavedGame.CurrentRocketGlobalDamage + 0.01f));


        t_texts[3] = rocketWorthStage.ToString("n2");


        globalValuesOld[0] = SavedGame.CurrentRocketGlobalTimeLastStage;
        globalValuesOld[1] = SavedGame.CurrentRocketGlobalDamageLastStage;
        globalValuesOld[2] = SavedGame.CurrentRocketGlobalFuelLastStage;

        float rocketWorthSumOld = 0f;
        rocketWorthSumOld += SavedGame.RocketPrices[Statics.selectedRocket];
        for (int i = 0; i < LevelNumber.GetStage(Statics.currentLevel); i++)
        {
            rocketWorthSumOld += SavedGame.GetGlobalWorthStage(SavedGame.LastPlayedRocket, i);
        }

        float rocketWorthSumNew = rocketWorthSumOld + SavedGame.GetGlobalWorthStage(SavedGame.LastPlayedRocket, LevelNumber.GetStage(Statics.currentLevel));

        globalValuesOld[3] = rocketWorthSumOld;// SavedGame.GetGlobalWorthStage(Statics.selectedRocket, LevelNumber.GetStage(LevelNumber.GetFirstLevelOfStage(LevelNumber.GetStage(Statics.currentLevel)) - 1));//CalculateRocketWorth(globalValuesOld[0], globalValuesOld[1], globalValuesOld[2], (LevelNumber.GetFirstLevelOfStage(LevelNumber.GetStage(Statics.currentLevel))));
        globalValues[0] = SavedGame.CurrentRocketGlobalTime;
        globalValues[1] = SavedGame.CurrentRocketGlobalDamage;
        globalValues[2] = SavedGame.CurrentRocketGlobalFuel;
        globalValues[3] = rocketWorthSumNew;// CalculateRocketWorth(globalValues[0], globalValues[1], globalValues[2], Statics.currentLevel + 1);

        textMeshesGlobal[0].text = globalValuesOld[0].ToString("n3");
        textMeshesGlobal[1].text = globalValuesOld[1].ToString();
        textMeshesGlobal[2].text = globalValuesOld[2].ToString("n2");
        textMeshesGlobal[3].text = globalValuesOld[3].ToString("n2");

        if (Statics.isInFreestyle)
        {
            levelText.text = "Planet Summary (Freestyle)";
        }
        else
        {
            levelText.text = "Planet Summary";

            //Debug.Log("Received money");
            //SavedGame.Money += (int)rocketWorthStage;
        }






        //t_texts[5] = "Time: " + SavedGame.CurrentRocketGlobalTime.ToString("#.###");

        /*moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + SavedGame.CurrentRocketGlobalTime.ToString("#.###");
        totalDamageText.text = "Total Damage: " + SavedGame.CurrentRocketGlobalDamage.ToString();

        currentRocketWorth = worthFaktor / (SavedGame.CurrentRocketGlobalTime * (SavedGame.CurrentRocketGlobalDamage + 0.01f));

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString("#.##");*/

    }

    public static float CalculateRocketWorth(float time, float damage, float fuel, int levelsDone, int[] whichLevels)
    {
        Debug.Log("Levels done: " + levelsDone.ToString());
        float sum = 0f;

        for (int i = 0; i < whichLevels.Length; i++)
        {
            sum += ((worthFactor * PlanetsWorth.PlanetsFactors[whichLevels[i]]) / (time * timeFactor + damage * damageFactor + fuel * fuelFactor)) * 10 * SavedGame.RocketMultiplier[Statics.selectedRocket];

            if (SavedGame.ChallengeRewards[SavedGame.LastPlayedRocket, whichLevels[i]] > 0f)
            {
                sum += SavedGame.ChallengeRewards[SavedGame.LastPlayedRocket, whichLevels[i]];
            }
        }

        if (float.IsInfinity(sum) || float.IsNegativeInfinity(sum) || float.IsPositiveInfinity(sum) || float.IsNaN(sum) || sum < -100000f || sum > 100000f)
        {
            // Something went wrong at calculating
            sum = 0f;
        }

        return sum;// ((worthFactor * levelsDone) / (time * timeFactor + damage * damageFactor + fuel * fuelFactor)) * 10 * SavedGame.RocketMultiplier[Statics.selectedRocket];
    }

    public void ButtonContinueClick()
    {
        SavedGame.SaveSavegame();
        SceneManager.LoadScene(StaticsSingleplayer.GetSceneToLoad(Statics.nextScene));
    }

    public void ButtonBackToMenuClick()
    {
        SavedGame.SaveSavegame();
        SceneManager.LoadScene("Main_Menu_3");
    }

    public void ButtonSellRocketClick()
    {
        SavedGame.SaveSavegame();
        Statics.startGarageLeft = true;
        SceneManager.LoadScene("Garage");
    }

    /*private void Start()
    {
        moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + timeFaktor.ToString();
        totalDamageText.text = "Total Damage: " + damageFaktor.ToString();

        currentRocketWorth = (worthFaktor / timeFaktor) - damageFaktor;

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString();

    }*/

}
