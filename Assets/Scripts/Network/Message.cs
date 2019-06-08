using UnityEngine;
using System.Collections;

public class Message {

    public byte[] data;
    public byte numb = 0;
    public bool ack = false;
    public int repeatCoold = 0;
    public int repeatCount = 0;

    public Message(byte[] data)
    {
        this.data = data;
        numb = 0;
        ack = false;
        repeatCoold = 0;
    }

    public Message(byte[] data, byte numb)
    {
        this.data = data;
        this.numb = numb;
        ack = false;
        repeatCoold = 0;
    }
}
