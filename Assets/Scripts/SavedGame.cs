using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGame
{
    // Config variables

    public const string savegameDir = ".";



    // SavedGame variables
    public static int LastSelectedRocket = 0;
    public static int CurrentLevelIndex = 0;
    public static bool[] OwnedRockets;
    public static int Money = 0;
    // Hier kannst noch so viele Sachen hinzufügen wie du willst


    public static void LoadSavegame()
    {
        if (Directory.Exists(savegameDir) == false)
        {
            Debug.LogError("Directory for savegame (" + savegameDir + ") doesn't exist");
        }
        else
        {

        }
    }



    private static void writeStringToFile(string text, string filePath)
    {

    }

    
}
