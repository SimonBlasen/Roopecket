using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGame
{
    // Config variables

    public const string savegameDir = "SaveGames";
    public const string savegameFile = "savegame.rpt";
    public const string seperator = "|";
    public const char seperatorC = '|';




    public static void SaveSavegame()
    {
        Debug.Log("Saving savegame...");
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
            con += "3=" + Money.ToString() + seperator;

            con += "4=";
            for (int i = 0; i < DrEberhardtFound.Length; i++)
            {
                con += DrEberhardtFound[i].ToString() + ",";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;

            con += "5=";
            for (int i = 0; i < RocketNames.Length; i++)
            {
                con += RocketNames[i] + ",";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;

            con += "6=";
            for (int i = 0; i < NextLevel.Length; i++)
            {
                con += NextLevel[i].ToString() + ",";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;

            con += "7=";
            for (int i = 0; i < CurrentDamageStage.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentDamageStage.GetLength(1); j++)
                {
                    con += CurrentDamageStage[i, j].ToString() + "_";
                }
                con += "-";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;

            con += "8=";
            for (int i = 0; i < CurrentTimeStage.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentTimeStage.GetLength(1); j++)
                {
                    con += CurrentTimeStage[i, j].ToString() + "_";
                }
                con += "-";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;

            con += "9=";
            for (int i = 0; i < CurrentUsedFuel.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentUsedFuel.GetLength(1); j++)
                {
                    con += CurrentUsedFuel[i, j].ToString() + "_";
                }
                con += "-";
            }
            con = con.Substring(0, con.Length - 1);
            con += seperator;

            File.WriteAllText(savegameDir + "/" + savegameFile, con);
        }
    }

    public static void LoadSavegame()
    {
        Debug.Log("Loading savegame...");
        if (Directory.Exists(savegameDir) == false)
        {
            Debug.LogError("Directory for savegame (" + savegameDir + ") doesn't exist");
        }
        else
        {
            string[] lines = File.ReadAllText(savegameDir + "/" + savegameFile).Split(seperatorC);

            for (int l = 0; l < lines.Length; l++)
            {
                if (lines[l].Length > 0)
                {
                    string[] con = lines[l].Split('=');

                    if (con[0] == "0")
                    {
                        LastSelectedRocket = Convert.ToInt32(con[1]);
                    }
                    else if (con[0] == "1")
                    {
                        CurrentLevelIndex = Convert.ToInt32(con[1]);
                    }
                    else if (con[0] == "2")
                    {
                        string[] els = con[1].Split(',');
                        for (int i = 0; i < els.Length; i++)
                        {
                            OwnedRockets[i] = Convert.ToInt32(els[i]);
                        }
                    }
                    else if (con[0] == "3")
                    {
                        Money = Convert.ToInt32(con[1]);
                    }
                    else if (con[0] == "4")
                    {
                        string[] els = con[1].Split(',');
                        for (int i = 0; i < els.Length; i++)
                        {
                            DrEberhardtFound[i] = Convert.ToBoolean(els[i]);
                        }
                    }
                    else if (con[0] == "5")
                    {
                        string[] els = con[1].Split(',');
                        for (int i = 0; i < els.Length; i++)
                        {
                            RocketNames[i] = els[i];
                        }
                    }
                    else if (con[0] == "6")
                    {
                        string[] els = con[1].Split(',');
                        for (int i = 0; i < els.Length; i++)
                        {
                            NextLevel[i] = Convert.ToInt32(els[i]);
                        }
                    }
                    else if (con[0] == "7")
                    {
                        string[] els = con[1].Split('-');
                        for (int i = 0; i < els.Length; i++)
                        {
                            for (int j = 0; j < els[i].Split('_').Length; j++)
                            {
                                if (els[i].Split('_')[j].Length > 0)
                                    CurrentDamageStage[i, j] = Convert.ToSingle(els[i].Split('_')[j]);
                            }
                        }
                    }
                    else if (con[0] == "8")
                    {
                        string[] els = con[1].Split('-');
                        for (int i = 0; i < els.Length; i++)
                        {
                            for (int j = 0; j < els[i].Split('_').Length; j++)
                            {
                                if (els[i].Split('_')[j].Length > 0)
                                    CurrentTimeStage[i, j] = Convert.ToSingle(els[i].Split('_')[j]);
                            }
                        }
                    }
                    else if (con[0] == "9")
                    {
                        string[] els = con[1].Split('-');
                        for (int i = 0; i < els.Length; i++)
                        {
                            for (int j = 0; j < els[i].Split('_').Length; j++)
                            {
                                if (els[i].Split('_')[j].Length > 0)
                                    CurrentUsedFuel[i, j] = Convert.ToSingle(els[i].Split('_')[j]);
                            }
                        }
                    }
                }
            }
        }
    }


    public static bool firstStart = true;


    // SavedGame variables
    public static int LastSelectedRocket = 0;           // 0
    public static int CurrentLevelIndex = 0;            // 1
    //public static bool[] OwnedRockets = new bool[32];   // 2
    public static int Money = 0;                        // 3
    //public static float CurrentLevelTime;               // Fürs speichern der bisher geflogenen Zeit mit einer Rakete (oder machst du das seperat in die Raketen rein?)
    //public static int collectedProfs;                   // Für die im Hintergrund angeklickten Profs (Profs haben schon ein Script)
    // Hier kannst noch so viele Sachen hinzufügen wie du willst

        

    public static int[] RocketPrices = new int[]
        {
            100,
            250,
            300,
            1500,
            99,
        };


    public static bool[] DrEberhardtFound = new bool[256];

    public static int[] OwnedRockets = new int[256];
    public static string[] RocketNames = new string[256];   // May not include "," and speerator
    public static int[] NextLevel = new int[256];
    //public static float[] CurrentDamage = new float[256];
    //public static float[] CurrentTime = new float[256];
    public static float[,] CurrentDamageStage = new float[256,20];
    public static float[,] CurrentTimeStage = new float[256,20];
    public static float[,] CurrentUsedFuel = new float[256, 20];

    public static float FreestyleTime;
    public static float FreestyleDamage;
    public static float FreestyleFuel;


    public static void DeleteFile()
    {
        File.Delete(savegameDir + "/" + savegameFile);
    }




    public static float CurrentRocketGlobalTime
    {
        get
        {
            return GetGlobalTime(Statics.selectedRocket);
            //return CurrentTime[Statics.selectedRocket];
        }
    }

    public static float CurrentRocketGlobalDamage
    {
        get
        {
            return GetGlobalDamage(Statics.selectedRocket);
            //return CurrentDamage[Statics.selectedRocket];
        }
    }

    public static float CurrentRocketGlobalFuel
    {
        get
        {
            return GetGlobalFuel(Statics.selectedRocket);
            //return CurrentDamage[Statics.selectedRocket];
        }
    }

    public static float CurrentRocketGlobalTimeLastStage
    {
        get
        {
            return GetGlobalTimeLastStage(Statics.selectedRocket);
        }
    }

    public static float CurrentRocketGlobalDamageLastStage
    {
        get
        {
            return GetGlobalDamageLastStage(Statics.selectedRocket);
        }
    }

    public static float CurrentRocketGlobalFuelLastStage
    {
        get
        {
            return GetGlobalFuelLastStage(Statics.selectedRocket);
        }
    }

    public static float GetGlobalFuel(int rocket)
    {
        float sum = 0f;
        for (int i = 0; i < CurrentUsedFuel.GetLength(1); i++)
        {
            if (i < NextLevel[rocket])
            {
                sum += CurrentUsedFuel[rocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalFuelForStage(int stage)
    {

        float sum = 0f;
        for (int i = 0; i < CurrentUsedFuel.GetLength(1); i++)
        {
            if (LevelNumber.GetStage(i) == stage && i < NextLevel[Statics.selectedRocket])
            {
                sum += CurrentUsedFuel[Statics.selectedRocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalDamageForStage(int stage)
    {

        float sum = 0f;
        for (int i = 0; i < CurrentDamageStage.GetLength(1); i++)
        {
            if (LevelNumber.GetStage(i) == stage && i < NextLevel[Statics.selectedRocket])
            {
                sum += CurrentDamageStage[Statics.selectedRocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalTimeForStage(int stage)
    {

        float sum = 0f;
        for (int i = 0; i < CurrentTimeStage.GetLength(1); i++)
        {
            if (LevelNumber.GetStage(i) == stage && i < NextLevel[Statics.selectedRocket])
            {
                sum += CurrentTimeStage[Statics.selectedRocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalFuelLastStage(int rocket)
    {
        float sum = 0f;
        for (int i = 0; i < CurrentUsedFuel.GetLength(1); i++)
        {
            if (LevelNumber.GetStage(i) < LevelNumber.GetStage(Statics.currentLevel) && i < NextLevel[rocket])
            {
                sum += CurrentUsedFuel[rocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalTime(int rocket)
    {
        float sum = 0f;
        for (int i = 0; i < CurrentTimeStage.GetLength(1); i++)
        {
            if (i < NextLevel[rocket])
            {
                sum += CurrentTimeStage[rocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalTimeLastStage(int rocket)
    {
        float sum = 0f;
        for (int i = 0; i < CurrentTimeStage.GetLength(1); i++)
        {
            if (LevelNumber.GetStage(i) < LevelNumber.GetStage(Statics.currentLevel) && i < NextLevel[rocket])
            {
                sum += CurrentTimeStage[rocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalDamage(int rocket)
    {
        float sum = 0f;
        for (int i = 0; i < CurrentDamageStage.GetLength(1); i++)
        {
            if (i < NextLevel[rocket])
            {
                sum += CurrentDamageStage[rocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float GetGlobalDamageLastStage(int rocket)
    {
        float sum = 0f;
        for (int i = 0; i < CurrentDamageStage.GetLength(1); i++)
        {
            if (LevelNumber.GetStage(i) < LevelNumber.GetStage(Statics.currentLevel) && i < NextLevel[rocket])
            {
                sum += CurrentDamageStage[rocket, i];
            }
            else
            {
            }
        }

        return sum;
    }

    public static float CurrentRocketStageTime
    {
        get
        {
            float sum = 0f;
            for (int i = 0; i < CurrentTimeStage.GetLength(1); i++)
            {
                if (LevelNumber.GetStage(i) == LevelNumber.GetStage(Statics.currentLevel) && i < NextLevel[Statics.selectedRocket])
                {
                    sum += CurrentTimeStage[Statics.selectedRocket, i];
                }
                else
                {
                }
            }

            return sum;
        }
    }

    public static float CurrentRocketStageDamage
    {
        get
        {
            float sum = 0f;
            for (int i = 0; i < CurrentDamageStage.GetLength(1); i++)
            {
                if (LevelNumber.GetStage(i) == LevelNumber.GetStage(Statics.currentLevel) && i < NextLevel[Statics.selectedRocket])
                {
                    sum += CurrentDamageStage[Statics.selectedRocket, i];
                }
                else
                {
                }
            }

            return sum;
        }
    }

    public static float CurrentRocketStageFuel
    {
        get
        {
            float sum = 0f;
            for (int i = 0; i < CurrentUsedFuel.GetLength(1); i++)
            {
                if (LevelNumber.GetStage(i) == LevelNumber.GetStage(Statics.currentLevel) && i < NextLevel[Statics.selectedRocket])
                {
                    sum += CurrentUsedFuel[Statics.selectedRocket, i];
                }
                else
                {
                }
            }

            return sum;
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
