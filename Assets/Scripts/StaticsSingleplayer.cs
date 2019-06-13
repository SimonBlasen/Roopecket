using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticsSingleplayer : MonoBehaviour
{
    protected static float levelChunkBeginTime = 0f;
    protected static float damageTaken = 0f;


    private static StaticsSingleplayer inst = null;

    public void AddDamage(float damage)
    {
        damageTaken += damage;
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
}
