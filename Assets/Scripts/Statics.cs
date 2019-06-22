using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics
{
    public static string ip = "192.168.1.25";
    public static int port = 28000;

    public static int currentLevel = 0;
    public static int selectedRocket = 0;
    public static int selectedRocketP2 = 0;
    public static byte selectedMap = 0;
    public static bool sendMapToServer = false;
    public static ulong Steam64ID = 0;
    public static string SteamName = "No Steam name";
    public static bool isInFreestyle = false;

    public static string nextScene;

    public static int comicSceneToLoad = 1;
}
