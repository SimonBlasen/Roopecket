using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class SappNetwork {

    private const int recentAckSize = 30;

    private UdpClient udpServer;

    private List<Message> messages;
    private List<Message> udpMessages;

    private List<byte> recentAcks = new List<byte>();
    private List<Message> sentRelUdp;
    private byte relCount = 0;
    private byte relAck = 0;
    private Thread thread;

    private byte[] last0_14 = new byte[] { };
    

    public SappNetwork()
    {
        messages = new List<Message>();
        udpMessages = new List<Message>();
        sentRelUdp = new List<Message>();
    }

    public bool Connect(string ip, int port)
    {
        return false;
    }

    public bool Connect(string ip, int port, int udpPort)
    {
        return false;
        //return server.Connect();
    }

    public bool ConnectOnlyUdp(string ip, int udpPort)
    {
        try
        {
            udpServer = new UdpClient(ip, udpPort);
        }
        catch
        {
            udpServer = null;
            return false;
        }
        
        udpServer.RecieveUdpData += UdpServer_RecieveUdpData;


        thread = new Thread(new ThreadStart(Process));
        thread.Start();
        return true;
    }

    public void Shutdown()
    {
        if (thread != null)
        {
            thread.Abort();
        }
        if (udpServer != null)
        {
            udpServer.Shutdown();
        }
    }

    private void UdpServer_RecieveUdpData(byte[] datas)
    {
        bool okay = true;

        okay = okay & (datas.Length > 2);

        if (okay)
        {
            int messageLength = 0;

            byte[] splitData;
            int offset = 0;
            do
            {
                messageLength = (datas[offset] << 8) | datas[offset + 1];
                splitData = new byte[messageLength];
                for (int i = 0; i < messageLength; i++)
                {
                    splitData[i] = datas[offset + i + 2];
                }

                //ReceiveMessage(splitData);

                // Is Ack
                if (splitData.Length == 3 && splitData[0] == 255  && splitData[1] == 255)
                {
#if DEBUG_ENABLED
                    Debug.Log("Got ACK [" + splitData[2] + "]");
#endif
                    for (int i = 0; i < sentRelUdp.Count; i++)
                    {
                        if (sentRelUdp[i].numb == splitData[2])
                        {
#if DEBUG_ENABLED
                            Debug.Log("DID NOT!!!");
#endif
                            //sentRelUdp[i].ack = true;
                            sentRelUdp.RemoveAt(i);

                            //relAck = splitData[2];
                            //relAck++;

                            break;
                        }
                    }
                }
                else
                {
                    //Debug.Log("Got UDP, but no ACK");

                    byte[] onlyData = splitData;

                    if (splitData.Length >= 3 && splitData[0] == 255 && splitData[1] == 253)
                    {
#if DEBUG_ENABLED
                        Debug.Log("Got RelUDP [" + splitData[2] + "]");
#endif

                        SendUdp(new byte[] { 255, 252, splitData[2] });

                        if (recentAcks.Contains(splitData[2]))
                        {
                            onlyData = null;
                        }
                        else
                        {
                            recentAcks.Add(splitData[2]);

                            onlyData = new byte[splitData.Length - 3];
                            for (int i = 0; i < onlyData.Length; i++)
                            {
                                onlyData[i] = splitData[i + 3];
                            }
                        }

                    }

                    if (onlyData != null)
                    {
                        if (onlyData.Length >= 3 && onlyData[0] == 0 && onlyData[1] == 14)
                        {
                            last0_14 = onlyData;
                        }
                        else
                        {
                            udpMessages.Add(new Message(onlyData));
                        }
                    }
                }


                offset += messageLength + 2;

            } while (offset < datas.Length);
        }
    }

    public byte[] Last0_14
    {
        get
        {
            return last0_14;
        }
    }

    public void Disconnect()
    {
        udpServer.Shutdown();
    }

    public bool HasMessage
    {
        get
        {
            return false;
        }
    }

    public bool HasUdpMessage
    {
        get
        {
            return udpMessages.Count > 0;
        }
    }

    public byte[] GetMessage()
    {
        if (HasMessage)
        {
            byte[] data;
            if (messages[0] != null && messages[0].data != null)
                data = messages[0].data;
            else
                data = null;

            messages.RemoveAt(0);

            return data;
        }
        else
        {
            return null;
        }
    }

    public byte[] GetUdpMessage()
    {
        if (HasUdpMessage)
        {
            byte[] data;
            if (udpMessages[0] != null && udpMessages[0].data != null)
                data = udpMessages[0].data;
            else
                data = null;

            udpMessages.RemoveAt(0);

            return data;
        }
        else
        {
            return null;
        }
    }

    public void Send(byte[] data)
    {
        byte[] dataWithLength = new byte[data.Length + 2];
        dataWithLength[0] = (byte)(data.Length >> 8);
        dataWithLength[1] = (byte)data.Length;
        for (int i = 0; i < data.Length; i++)
        {
            dataWithLength[i + 2] = data[i];
        }

        //TODO included if for singleplayer
        //if (server != null)
        //    server.send(dataWithLength);
    }

    public void SendUdp(byte[] data)
    {
        /*byte[] dataWithLength = new byte[data.Length + 2];
        dataWithLength[0] = (byte)(data.Length >> 8);
        dataWithLength[1] = (byte)data.Length;
        for (int i = 0; i < data.Length; i++)
        {
            dataWithLength[i + 2] = data[i];
        }*/

        //TODO included if for singleplayer
        if (udpServer != null)
            udpServer.Send(data);
    }

    public bool SendUdpRel(byte[] data)
    {
        if (udpServer != null/* && relCount != relAck - 1*/)
        {
            sentRelUdp.Add(new Message(data, relCount));

            udpServer.Send(getRelBytes(data, relCount));

            relCount++;

#if DEBUG_ENABLED
            Debug.Log("RelUDP sent");
#endif

            return true;
        }
        return false;
    }

    public byte RelCount
    {
        get
        {
            return relCount;
        }
        set
        {
            relCount = value;
        }
    }

    public byte RelAck
    {
        get
        {
            return relAck;
        }
        set
        {
            relAck = value;
        }
    }

    private void Server_RecieveData(Client sender, string data, byte[] datas)
    {
        if (lengthing)
        {
            bool okay = true;

            okay = okay & (datas.Length > 2);

            if (okay)
            {
                int messageLength = 0;

                byte[] splitData;
                int offset = 0;
                do
                {
                    messageLength = (datas[offset] << 8) | datas[offset + 1];
                    splitData = new byte[messageLength];
                    for (int i = 0; i < messageLength; i++)
                    {
                        splitData[i] = datas[offset + i + 2];
                    }

                    //ReceiveMessage(splitData);
                    messages.Add(new Message(splitData));

                    offset += messageLength + 2;

                } while (offset < datas.Length);
            }
        }
        else
        {
            messages.Add(new Message(datas));
        }
    }

    private bool lengthing = true;

    public bool EnableLengthen
    {
        get
        {
            return lengthing;
        }
        set
        {
            lengthing = value;
        }
    }

    private void Process()
    {
        int noAckTime = 0;
        int sleepTime = 50;
        while (true)
        {
            if (sentRelUdp.Count > 0)
            {
                for (int i = 0; i < sentRelUdp.Count; i++)
                {
                    sentRelUdp[i].repeatCoold += sleepTime;
                    if (sentRelUdp[i].repeatCoold >= 300)
                    {
                        udpServer.Send(getRelBytes(sentRelUdp[i].data, sentRelUdp[i].numb));
#if DEBUG_ENABLED
                        Debug.Log("RelUDP resent [" + sentRelUdp[i].numb + "]");
#endif

                        sentRelUdp[i].repeatCoold = 0;
                        sentRelUdp[i].repeatCount++;
                        if (sentRelUdp[i].repeatCount > 6)
                        {
                            sentRelUdp.RemoveAt(i);
                            i--;
#if DEBUG_ENABLED
                            Debug.Log("6 times didnt get it to send");
#endif
                        }
                    }
                }

            }

            
                while (recentAcks.Count > recentAckSize)
                {
                    recentAcks.RemoveAt(0);
                }

            Thread.Sleep(sleepTime);
        }
    }


    private byte[] getRelBytes(byte[] data, byte relIndex)
    {
        byte[] relData = new byte[data.Length + 3];
        relData[0] = 255;
        relData[1] = 254;
        relData[2] = relIndex;
        for (int i = 0; i < data.Length; i++)
        {
            relData[3 + i] = data[i];
        }

        return relData;
    }
}
