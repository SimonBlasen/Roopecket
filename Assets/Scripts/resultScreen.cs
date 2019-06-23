using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum EndscreenState
{
    START = 255, STAGE_TIME = 0, STAGE_DAMAGE = 1, STAGE_FUEL = 2, STAGE_WORTH = 3, WAIT_STAGE_TO_GLOBAL = 4, GLOBAL_TIME = 5, GLOBAL_DAMAGE = 6, GLOBAL_FUEL = 7, GLOBAL_WORTH = 8,
    WAIT_STAGE_TIME = 257, WAIT_STAGE_DAMAGE = 258, WAIT_STAGE_FUEL = 259, WAIT_STAGE_WORTH = 260, WAIT_GLOBAL_TIME = 261, WAIT_GLOBAL_DAMAGE = 262, WAIT_GLOBAL_FUEL = 263, WAIT_GLOBAL_WORTH = 264,
    END = 256
}

public static class EndscreenStateMethods
{
    public static float Time(this EndscreenState state)
    {
        float textTime = 1.5f;
        float textAddTime = 1f;

        float textWaitsBetween = 1f;

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
                return 5f;
            case EndscreenState.GLOBAL_TIME:
                return textAddTime;
            case EndscreenState.GLOBAL_DAMAGE:
                return textAddTime;
            case EndscreenState.GLOBAL_FUEL:
                return textAddTime;
            case EndscreenState.GLOBAL_WORTH:
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
            default:
                return 1000f;
        }
    }

    public static EndscreenState NextState(this EndscreenState state)
    {
        Debug.Log("State was: " + state.ToString());
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
            default:
                return EndscreenState.END;
        }
    }
}

public class resultScreen : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    public TextMeshProUGUI moneyText, totalDamageText, DamageText, totalTimeText, timeText, fuelText, totalFuelText, rocketWorth, rocketWorthTotal;
    public TextMeshProUGUI[] textMeshes;
    public TextMeshProUGUI[] textMeshesGlobal;
    private string[] t_texts;
    private float[] globalValues;
    private float[] globalValuesOld;
    private string t_damage, t_damageTotal, t_time, t_timeTotal, t_fuel, t_fuelTotal, t_worth, t_worthTotal;
    private float currentRocketWorth;
    private const float damageFactor = 1f;
    private const float timeFactor = 1f;
    private const float fuelFactor = 1f;
    private const float worthFactor = 1200f;

    private EndscreenState state = EndscreenState.END;
    private float counter = 0f;

    private void Start()
    {
        t_texts = new string[textMeshes.Length];
        globalValues = new float[textMeshes.Length];
        globalValuesOld = new float[textMeshes.Length];
        GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
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
                        textMeshesGlobal[index].text = globalValues[index].ToString(toStringFormat);

                        state = state.NextState();
                        counter = 0f;
                    }
                    else
                    {
                        float perc = counter / state.Time();

                        float val = globalValuesOld[index] + (globalValues[index] - globalValuesOld[index]) * perc;

                        textMeshesGlobal[index].text = val.ToString(toStringFormat);
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
    }

    public void showEndscreenSimple()
    {
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
        globalValues[0] = SavedGame.CurrentTimeStage[Statics.selectedRocket, Statics.currentLevel];
        globalValues[1] = SavedGame.CurrentDamageStage[Statics.selectedRocket, Statics.currentLevel];
        globalValues[2] = SavedGame.CurrentUsedFuel[Statics.selectedRocket, Statics.currentLevel];
        globalValues[3] = CalculateRocketWorth(globalValues[0], globalValues[1], globalValues[2], 1);

        textMeshesGlobal[0].text = globalValuesOld[0].ToString("n3");
        textMeshesGlobal[1].text = globalValuesOld[1].ToString();
        textMeshesGlobal[2].text = globalValuesOld[2].ToString("n2");
        textMeshesGlobal[3].text = globalValuesOld[3].ToString("n2");

        levelText.text = "Level " + (Statics.currentLevel + 1).ToString();
    }

    public void showEndScreen()
    {
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

        float rocketWorthStage = CalculateRocketWorth(SavedGame.CurrentRocketStageTime, SavedGame.CurrentRocketStageDamage, SavedGame.CurrentRocketStageFuel, 1 + Statics.currentLevel - (LevelNumber.GetFirstLevelOfStage(LevelNumber.GetStage(Statics.currentLevel))));
        //float rocketWorthStage = worthFaktor / (SavedGame.CurrentRocketGlobalTime * (SavedGame.CurrentRocketGlobalDamage + 0.01f));
        

        t_texts[3] = rocketWorthStage.ToString("n2");


        globalValuesOld[0] = SavedGame.CurrentRocketGlobalTimeLastStage;
        globalValuesOld[1] = SavedGame.CurrentRocketGlobalDamageLastStage;
        globalValuesOld[2] = SavedGame.CurrentRocketGlobalFuelLastStage;
        globalValuesOld[3] = CalculateRocketWorth(globalValuesOld[0], globalValuesOld[1], globalValuesOld[2], (LevelNumber.GetFirstLevelOfStage(LevelNumber.GetStage(Statics.currentLevel))));
        globalValues[0] = SavedGame.CurrentRocketGlobalTime;
        globalValues[1] = SavedGame.CurrentRocketGlobalDamage;
        globalValues[2] = SavedGame.CurrentRocketGlobalFuel;
        globalValues[3] = CalculateRocketWorth(globalValues[0], globalValues[1], globalValues[2], Statics.currentLevel + 1);

        textMeshesGlobal[0].text = globalValuesOld[0].ToString("n3");
        textMeshesGlobal[1].text = globalValuesOld[1].ToString();
        textMeshesGlobal[2].text = globalValuesOld[2].ToString("n2");
        textMeshesGlobal[3].text = globalValuesOld[3].ToString("n2");




        //t_texts[5] = "Time: " + SavedGame.CurrentRocketGlobalTime.ToString("#.###");

        /*moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + SavedGame.CurrentRocketGlobalTime.ToString("#.###");
        totalDamageText.text = "Total Damage: " + SavedGame.CurrentRocketGlobalDamage.ToString();

        currentRocketWorth = worthFaktor / (SavedGame.CurrentRocketGlobalTime * (SavedGame.CurrentRocketGlobalDamage + 0.01f));

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString("#.##");*/

    }

    public static float CalculateRocketWorth(float time, float damage, float fuel, int levelsDone)
    {
        Debug.Log("Levels done: " + levelsDone.ToString());
        return (worthFactor * levelsDone) / (time * timeFactor + damage * damageFactor + fuel * fuelFactor);
    }

    public void ButtonContinueClick()
    {
        SceneManager.LoadScene(Statics.nextScene);
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
