using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGame
{
    // Config variables

    public const string savegameDir = ".";
    public const string savegameFile = "savegame.rpt";
    public const string seperator = "|";



    // SavedGame variables
    public static int LastSelectedRocket = 0;           // 0
    public static int CurrentLevelIndex = 0;            // 1
    //public static bool[] OwnedRockets = new bool[32];   // 2
    public static int Money = 0;                        // 3
    public static float CurrentLevelTime;               // Fürs speichern der bisher geflogenen Zeit mit einer Rakete (oder machst du das seperat in die Raketen rein?)
    public static int collectedProfs;                   // Für die im Hintergrund angeklickten Profs (Profs haben schon ein Script)
    // Hier kannst noch so viele Sachen hinzufügen wie du willst

    public static bool[] DrEberhardtFound = new bool[256];

    public static int[] OwnedRockets = new int[256];
    public static string[] RocketNames = new string[256];
    public static int[] NextLevel = new int[256];
    public static float[] CurrentDamage = new float[256];
    public static float[] CurrentTime = new float[256];

    public static float CurrentRocketGlobalTime
    {
        get
        {
            return 0f;
        }
    }

    public static float CurrentRocketGlobalDamage
    {
        get
        {
            return 0f;
        }
    }

    public static float CurrentRocketStageTime
    {
        get
        {
            return 0f;
        }
    }

    public static float CurrentRocketStageDamage
    {
        get
        {
            return 0f;
        }
    }


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

    public static void SaveSavegame()
    {
        if (Directory.Exists(savegameDir) == false)
        {
            Debug.LogError("Directory for savegame (" + savegameDir + ") doesn't exist");
        }
        else
        {
            string con = "";

            con += "0=" + LastSelectedRocket.ToString() + seperator;
            con += "1=" + CurrentLevelIndex.ToString() + seperator;
            con += "2=";
            for (int i = 0; i < OwnedRockets.Length; i++)
            {
                con += OwnedRockets[i].ToString() + ",";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;
            con += "3=" + Money.ToString();
        }
    }



    private static void writeStringToFile(string text, string filePath)
    {
        File.WriteAllText(filePath, text);
    }

    private static string readStringFromFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    
}
