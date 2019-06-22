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
                    if (rocketController.Thrusts[i])
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


    public void ReachedFinish()
    {
        float takenDamage = StaticsSingleplayer.ReadTakenDamage();
        float tookTime = StaticsSingleplayer.ReadTimer();

        if (Statics.isInFreestyle == false)
        {
            SavedGame.CurrentTimeStage[Statics.selectedRocket, levelNumber.levelNumber] = tookTime;
            SavedGame.CurrentDamageStage[Statics.selectedRocket, levelNumber.levelNumber] = takenDamage;


        }
    }
}
