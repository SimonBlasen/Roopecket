using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    private RocketProps rocketProps;
    private RocketController rocketController;
    private RocketSpawner rs;

    private LevelNumber levelNumber;

    private bool tkStarted = false;
    private bool reachedFinish = false;
    private float timeInFinish = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
        levelNumber = GameObject.FindObjectOfType<LevelNumber>();

        StaticsSingleplayer.Reset();
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
                    rocketController = rs.SpawnedRocket.GetComponent<RocketController>();
                }
            }
            else
            {
                rocketProps = GameObject.FindObjectOfType<RocketProps>();
                rocketController = rocketProps.transform.GetComponent<RocketController>();
            }
        }
        else
        {
            if (tkStarted == false)
            {
                for (int i = 0; i < rocketController.Thrusts.Length; i++)
                {
                    if (rocketController.Thrusts[i] > 0f)
                    {
                        tkStarted = true;
                        StaticsSingleplayer.StartTimer();

                        Debug.Log("Level Timer started");

                        break;
                    }
                }
            }
        }
    }

    public float GetCurrentTime()
    {
        if (tkStarted == false)
        {
            return 0f + Statics.resetMultiplier;
        }
        else
        {
            if (reachedFinish == false)
            {
                return StaticsSingleplayer.ReadTimer() + Statics.resetMultiplier;
            }
            else
            {
                return timeInFinish + Statics.resetMultiplier;
            }
        }
    }


    public void ReachedFinish()
    {
        reachedFinish = true;
        float takenDamage = StaticsSingleplayer.ReadTakenDamage();
        float tookTime = StaticsSingleplayer.ReadTimer();
        float usedFuel = StaticsSingleplayer.ReadFuelUsed();
        timeInFinish = tookTime + Statics.resetMultiplier;

        if (Statics.isInFreestyle == false)
        {
            SavedGame.CurrentTimeStage[SavedGame.LastPlayedRocket, levelNumber.levelNumber] = tookTime;
            SavedGame.CurrentDamageStage[SavedGame.LastPlayedRocket, levelNumber.levelNumber] = takenDamage;
            SavedGame.CurrentUsedFuel[SavedGame.LastPlayedRocket, levelNumber.levelNumber] = usedFuel;
        }
        else
        {
            SavedGame.FreestyleTime = tookTime;
            SavedGame.FreestyleDamage = takenDamage;
            SavedGame.FreestyleFuel = usedFuel;
        }
    }
}
