using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour
{
    [SerializeField]
    private float sendRate = 1/20f;
    [SerializeField]
    private Network network;


    private const int maxPlayers = 16;
    private ulong[] otherPlayersSteamIDs = new ulong[maxPlayers];
    private OtherPlayer[] otherPlayers = new OtherPlayer[maxPlayers];

    private float sendRateCounter = 0f;
    private bool connectedToServer = false;
    private bool didSendPlayerinfo = false;


    private Transform rocketTransform;
    private RocketProps rocketProps;
    private RocketController rocketController;
    private RocketSpawner rs;


    // Use this for initialization
    void Start()
    {
        rocketTransform = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();

        for (int i = 0; i < maxPlayers; i++)
        {
            otherPlayers[i].init = false;
            otherPlayersSteamIDs[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketTransform == null)
        {
            if (rs.SpawnedRocket != null)
            {
                rocketTransform = rs.SpawnedRocket.transform;
                rocketProps = rocketTransform.GetComponent<RocketProps>();
                rocketController = rocketTransform.GetComponent<RocketController>();
            }
        }


        if (connectedToServer)
        {
            /*if (didSendPlayerinfo == false)
            {
                didSendPlayerinfo = true;
                sendPlayerInfo(Statics.Steam64ID, Statics.SteamName, (short)Statics.selectedRocket);
            }*/

            sendRateCounter += Time.deltaTime;
            if (sendRateCounter >= sendRate)
            {
                sendRateCounter = 0f;
                sendOwnPose();
            }



            for (int i = 0; i < otherPlayers.Length; i++)
            {
                if (otherPlayers[i].init)
                {
                    Transform rocket = otherPlayers[i].rocket;
                    rocket.position = otherPlayers[i].position;
                    rocket.rotation = Quaternion.Euler(otherPlayers[i].rot);
                }
            }
        }
    }

    public byte OwnID
    {
        get;set;
    }


    private void sendOwnPose()
    {
        //ulong steam64ID = Statics.Steam64ID;
        uint metaState = 0x0;
        metaState |= (rocketController.LandingMoversOut ? (uint)0x01 : 0x00);

        uint counter = 0x01 << 16;

        for (int i = 0; i < rocketController.Thrusts.Length; i++)
        {
            if (rocketController.Thrusts[i])
            {
                metaState |= counter;
            }
            counter = counter << 1;
        }

        network.SendPose(metaState, rocketTransform.position, rocketTransform.rotation.eulerAngles);
    }


    public void SetOtherplayerName(byte playerID, ulong steam64ID, string name, short rocketID)
    {
        Debug.Log("Spawning rocket");

        otherPlayers[playerID].init = true;
        otherPlayers[playerID].name = name;
        otherPlayers[playerID].steam64ID = steam64ID;
        otherPlayers[playerID].thrusters = new bool[16];
        otherPlayers[playerID].rocketID = rocketID;

        otherPlayers[playerID].rocket = rs.SpawnrocketNoRig(rocketID);
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
    /*
    public void SetOtherplayer(ulong steam64ID, Vector3 pos, Vector3 rot, uint metaState)
    {
        int playerID = getPlayerID(steam64ID);
        if (playerID != -1)
        {
            bool landingMoversOut = (metaState & (0x00000001)) != (0x00);
            bool[] thrusters = new bool[16];

            uint counter = (0x00000001 << 16);
            for (int i = 0; i < thrusters.Length; i++)
            {
                otherPlayers[playerID].thrusters[i] = (metaState & counter) != (0x00);
                //thrusters[i] = (metaState & counter) != (0x00);
                counter = counter << 1;
            }


            otherPlayers[playerID].position = pos;
            otherPlayers[playerID].rot = rot;
            otherPlayers[playerID].landingMoversOut = landingMoversOut;
        }
        else
        {
            // Dont know that player yet

        }
    }
    */
    public void SetOtherplayer(int playerID, Vector3 pos, Vector3 rot, uint metaState)
    {
        if (playerID != OwnID && otherPlayers[playerID].init)
        {
            bool landingMoversOut = (metaState & (0x00000001)) != (0x00);
            bool[] thrusters = new bool[16];

            uint counter = (0x00000001 << 16);
            for (int i = 0; i < thrusters.Length; i++)
            {
                otherPlayers[playerID].thrusters[i] = (metaState & counter) != (0x00);
                //thrusters[i] = (metaState & counter) != (0x00);
                counter = counter << 1;
            }


            otherPlayers[playerID].position = pos;
            otherPlayers[playerID].rot = rot;
            otherPlayers[playerID].landingMoversOut = landingMoversOut;
        }
        else if (playerID != OwnID && otherPlayers[playerID].init == false)
        {
            // Dont know that player yet

            network.SendPlayerinfoRequest((byte)playerID);
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

    public bool ConnectedToServer
    {
        get
        {
            return connectedToServer;
        }
        set
        {
            connectedToServer = value;
        }
    }
}


public struct OtherPlayer
{
    public bool init;
    public ulong steam64ID;
    public string name;
    public Vector3 position;
    public Vector3 rot;
    public bool landingMoversOut;
    public bool[] thrusters;
    public short rocketID;
    public Transform rocket;
}