using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour
{

    private const int maxPlayers = 16;
    private ulong[] otherPlayersSteamIDs = new ulong[maxPlayers];
    private OtherPlayer[] otherPlayers = new OtherPlayer[maxPlayers];


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            otherPlayers[i].init = false;
            otherPlayersSteamIDs[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetOtherplayerName(ulong steam64ID, string name)
    {
        int playerID = getPlayerID(steam64ID);
        if (playerID == -1)
        {
            for (int i = 0; i < maxPlayers; i++)
            {
                if (otherPlayers[i].init == false)
                {
                    otherPlayersSteamIDs[i] = steam64ID;
                    playerID = i;
                    break;
                }
            }
        }

        otherPlayers[playerID].init = true;
        otherPlayers[playerID].name = name;
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
            if (otherPlayersSteamIDs[i] == steam64ID)
            {
                return i;
            }
        }
        return -1;
    }
}


public struct OtherPlayer
{
    public bool init;
    public string name;
    public Vector3 position;
    public Vector3 rot;
    public bool landingMoversOut;
    public bool[] thrusters;
}