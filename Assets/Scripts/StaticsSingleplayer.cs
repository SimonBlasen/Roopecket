using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticsSingleplayer : MonoBehaviour
{
    protected static float levelChunkBeginTime = 0f;
    protected static float damageTaken = 0f;
    protected static float fuelUsed = 0f;

    public static string[] levelNames = new string[]
        {
            "Platform First Level",
            "Platform sec Level",
            "Platform third Level",
            "Platform fourth Level",
            "Platform fivth Level",
            "Platform sixth Level",
            "Platform seventh Level",
            "Platform eighth Level",
            "Platform ninth Level",
            "Platform Level 10",
            "Platform Level 11",
            "Platform Level 12 CR",
            "Platform Level 13 CR",
            "Platform Level 14 CR space",
            "Platform Level 15",
            "Platform Level 16",
            "Platform Level 17",
            "Platform Level 18",
            "Platform Level 19",
            "Platform Level 20",
        };


    private static StaticsSingleplayer inst = null;

    public void AddDamage(float damage)
    {
        damageTaken += damage;
    }

    public static void UseFuel(float fuel)
    {
        fuelUsed += fuel;
    }

    private static void instM()
    {
        if (inst == null)
        {
            GameObject instGo = new GameObject("Statics Singleplayer");
            instGo.AddComponent<StaticsSingleplayer>();
            inst = instGo.GetComponent<StaticsSingleplayer>();
        }

    }

    public static StaticsSingleplayer Inst
    {
        get
        {
            instM();
            return inst;
        }
    }

    /// <summary>
    /// Starts the timer and resets all values
    /// </summary>
    public static void StartTimer()
    {
        instM();
        levelChunkBeginTime = Time.time;
        damageTaken = 0f;
        fuelUsed = 0f;
    }

    public static void Reset()
    {
        instM();
        damageTaken = 0f;
        fuelUsed = 0f;
    }

    /// <summary>
    /// Returns the time in seconds since you called StartTimer()
    /// </summary>
    /// <returns></returns>
    public static float ReadTimer()
    {
        instM();
        return Time.time - levelChunkBeginTime;
    }

    public static float ReadTakenDamage()
    {
        return damageTaken;
    }

    public static float ReadFuelUsed()
    {
        return fuelUsed;
    }
}
