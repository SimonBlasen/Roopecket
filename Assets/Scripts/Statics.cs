using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics
{
    public static string ip = "192.168.1.25";
    public static int port = 28000;

    public static int currentLevel = 0;
    public static int selectedRocket
    {
        get;set;
    }
    public static int selectedRocketP2 = 0;
    public static byte selectedMap = 0;
    public static bool sendMapToServer = false;
    public static ulong Steam64ID = 0;
    public static string SteamName = "No Steam name";
    public static bool isInFreestyle = false;

    public static float resetMultiplier = 0f;

    public static bool startGarageLeft = false;
    public static string nextScene;

    public static int comicSceneToLoad = 1;
    public static bool testingGarageRocket = false;

    public static int deviceP1 = 0;
    public static int deviceP2 = 0;
    public static bool isSplitscreen = false;

    public static int pvpRound = 0;
    public static int pvpScoreP1 = 0;
    public static int pvpScoreP2 = 0;

    public static KeyCode key1 = KeyCode.A;
    public static KeyCode key2 = KeyCode.S;
    public static KeyCode key3 = KeyCode.D;
    public static KeyCode key4 = KeyCode.F;
    public static KeyCode key5 = KeyCode.G;
    public static KeyCode keyLandingMovers = KeyCode.Space;
    public static KeyCode keyReset = KeyCode.R;
    public static KeyCode keySpecialLeft = KeyCode.LeftArrow;
    public static KeyCode keySpecialRight = KeyCode.RightArrow;

    public static bool movedCTFLogo = false;
}
