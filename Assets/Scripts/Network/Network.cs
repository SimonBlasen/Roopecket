using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    [SerializeField]
    private MultiManager multiManager;

    private SappNetwork network;
    
    private bool started = false;

    private float serverTimeout = 0f;

	// Use this for initialization
	void Start ()
    {
        network = new SappNetwork();

        string ip = "192.168.1.25";
        int port = 28000;
        
        
        network.ConnectOnlyUdp(ip, port);
        network.SendUdpRel(new byte[] { 0, 42, 0 });
        
        started = true;
	}

    void OnApplicationQuit()
    {
        Shutdown();
    }
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Shutdown();
        }

        //serverTimeout += Time.deltaTime;

        if (serverTimeout > 10f)
        {
            serverTimeout = 0f;
            // Timeout

            //SendSth();
        }

        if (network.HasUdpMessage)
        {
            serverTimeout = 0f;

            byte[] data = network.GetUdpMessage();
            if (data == null)
            {

            }
            else
            {
                // Got own id
                if (data.Length >= 2 && data[0] == 0 && data[1] == 2)
                {
                    Debug.Log("Got own id");

                    multiManager.OwnID = data[2];

                    List<byte> bytes = new List<byte>();

                    bytes.Add(0);
                    bytes.Add(1);
                    bytes.Add(multiManager.OwnID);
                    bytes.Add((byte)(Statics.Steam64ID >> 56));
                    bytes.Add((byte)(Statics.Steam64ID >> 48));
                    bytes.Add((byte)(Statics.Steam64ID >> 40));
                    bytes.Add((byte)(Statics.Steam64ID >> 32));
                    bytes.Add((byte)(Statics.Steam64ID >> 24));
                    bytes.Add((byte)(Statics.Steam64ID >> 16));
                    bytes.Add((byte)(Statics.Steam64ID >> 8));
                    bytes.Add((byte)(Statics.Steam64ID));
                    bytes.Add((byte)(Statics.selectedRocket >> 8));
                    bytes.Add((byte)Statics.selectedRocket);
                    bytes.Add((byte)(System.Text.Encoding.UTF8.GetBytes(Statics.SteamName).Length));
                    bytes.AddRange(System.Text.Encoding.UTF8.GetBytes(Statics.SteamName));

                    network.SendUdpRel(bytes.ToArray());
                }
            }
        }
	}


    public void SendPose(uint metaState, Vector3 pos, Vector3 rot)
    {
        byte[] xBytes = System.BitConverter.GetBytes(pos.x);
        byte[] yBytes = System.BitConverter.GetBytes(pos.y);
        byte[] zBytes = System.BitConverter.GetBytes(pos.z);
        byte[] xRBytes = System.BitConverter.GetBytes(rot.x);
        byte[] yRBytes = System.BitConverter.GetBytes(rot.y);
        byte[] zRBytes = System.BitConverter.GetBytes(rot.z);


        byte[] bytes = new byte[31];
        bytes[0] = 0;
        bytes[1] = 3;
        bytes[2] = multiManager.OwnID;
        bytes[3] = xBytes[0];
        bytes[4] = xBytes[1];
        bytes[5] = xBytes[2];
        bytes[6] = xBytes[3];
        bytes[7] = yBytes[0];
        bytes[8] = yBytes[1];
        bytes[9] = yBytes[2];
        bytes[10] = yBytes[3];
        bytes[11] = zBytes[0];
        bytes[12] = zBytes[1];
        bytes[13] = zBytes[2];
        bytes[14] = zBytes[3];
        bytes[15] = xRBytes[0];
        bytes[16] = xRBytes[1];
        bytes[17] = xRBytes[2];
        bytes[18] = xRBytes[3];
        bytes[19] = yRBytes[0];
        bytes[20] = yRBytes[1];
        bytes[21] = yRBytes[2];
        bytes[22] = yRBytes[3];
        bytes[23] = zRBytes[0];
        bytes[24] = zRBytes[1];
        bytes[25] = zRBytes[2];
        bytes[26] = zRBytes[3];
        bytes[27] = (byte)(metaState >> 24);
        bytes[28] = (byte)(metaState >> 16);
        bytes[29] = (byte)(metaState >> 8);
        bytes[30] = (byte)(metaState);

        network.SendUdp(bytes);
    }


    public void SendSth()
    {
        network.SendUdp(new byte[] { 0, 1, 2, 3 });
    }
    

    private void Shutdown()
    {
        Debug.Log("Disconnecting network");
        if (network != null)
        {
            network.SendUdp(new byte[] { 0, 43, 0 });
            network.Disconnect();
        }
    }
    


    private byte[] decat(byte[] data)
    {
        if (data.Length > 2)
        {
            byte[] newdata = new byte[data.Length - 2];

            for (int j = 0; j < newdata.Length; j++)
            {
                newdata[j] = data[j + 2];
            }

            return newdata;
        }
        else
        {
            return null;
        }
    }

    private byte[] concat(short pre, byte[] data)
    {
        byte[] newdata = new byte[data.Length + 2];
        newdata[0] = (byte)(pre >> 8);
        newdata[1] = (byte)pre;

        for (int j = 0; j < data.Length; j++)
        {
            newdata[j + 2] = data[j];
        }

        return newdata;
    }

    private string getNameFromByte(byte[] rawData)
    {
        byte[] sub = new byte[rawData.Length - 2];
        for (int i = 0; i < sub.Length; i++)
        {
            sub[i] = rawData[i + 2];
        }
        return System.Text.Encoding.Default.GetString(sub);
    }
}
