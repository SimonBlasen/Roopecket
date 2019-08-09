﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainmenuPlanetInfo : MonoBehaviour
{
    public int stageNumber = -1;
    public TextMeshPro textPlanet;
    public TextMeshPro textLeft;
    public TextMeshPro textValues;

    private float time;
    private float fuel;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        time = SavedGame.GetGlobalTimeForStage(stageNumber);
        damage = SavedGame.GetGlobalDamageForStage(stageNumber);
        fuel = SavedGame.GetGlobalFuelForStage(stageNumber);

        int amountOfLevels = 0;
        for (int i = 0; i < 20; i++)
        {
            if (LevelNumber.GetStage(i) == stageNumber && i < SavedGame.NextLevel[SavedGame.LastPlayedRocket])
            {
                amountOfLevels++;
            }
        }

        float rocketWorth = resultScreen.CalculateRocketWorth(time, damage, fuel, amountOfLevels);

        string worthnessString = rocketWorth.ToString("n2");
        if (float.IsNaN(rocketWorth) || float.IsInfinity(rocketWorth) || float.IsNegativeInfinity(rocketWorth) || float.IsPositiveInfinity(rocketWorth))
        {
            worthnessString = "0";
        }

        textValues.text = "" + "\n" +
                            "" + "\n" +
                            "                               " + time.ToString("n3") + "\n" + 
                            "                               " + damage.ToString("n0") + "\n" + 
                            "                               " + fuel.ToString("n2") + "\n" +
                            "                               " + "\n" +
                            "                               " + worthnessString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private bool shown = false;
    public bool Shown
    {
        get
        {
            return shown;
        }
        set
        {
            shown = value;

            textPlanet.GetComponent<MeshRenderer>().enabled = shown;
            textLeft.GetComponent<MeshRenderer>().enabled = shown;
            textValues.GetComponent<MeshRenderer>().enabled = shown;
        }
    }
}
