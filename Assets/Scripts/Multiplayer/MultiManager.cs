using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour
{

    private const int maxPlayers = 16;
    private ulong[] otherPlayersSteamIDs = new ulong[maxPlayers];
    private string[] otherPlayersNames = new string[maxPlayers];


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetOtherplayerName(ulong steam64ID, string name)
    {

    }


    /// <summary>
    /// 
    /// meta state:
    /// 0:          landing movers
    /// 16 - 31:    thrusters
    /// 
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="metaState"></param>

    public void SetOtherplayer(ulong steam64ID, Vector3 pos, Vector3 rot, uint metaState)
    {
        bool landingMoversOut = (metaState & (0x00000001)) != (0x00);
        bool[] thrusters = new bool[16];

        uint counter = (0x00000001 << 32);
        for (int i = 0; i < thrusters.Length; i++)
        {
            thrusters[i] = (metaState & counter) != (0x00);
            counter = counter << 1;
        }


    }

    private int getPlayerID(ulong steam64ID)
    {
        for (int i = 0; i < otherPlayersSteamIDs.Length; i++)
        {
            //TODO
        }
        return -1;
    }
}
