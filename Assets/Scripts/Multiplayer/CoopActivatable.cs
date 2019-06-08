using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopActivatable : MonoBehaviour
{

    private float[] times = new float[4048];
    private float[] values = new float[4048];
    private bool[] boolValues = new bool[4048];

    private int writeIndex = 0;
    private int readIndex = 0;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	protected void Update ()
    {
        if (Delay > 0f)
        {
            times[writeIndex] = Time.time + Delay;
            values[writeIndex] = Value;
            boolValues[writeIndex] = IsPressed;
            writeIndex++;
            if (writeIndex >= times.Length)
            {
                writeIndex = 0;
            }

            if (Time.time > times[readIndex])
            {
                handleValue(values[readIndex]);
                handleBoolValue(boolValues[readIndex]);
                readIndex++;
                if (readIndex >= times.Length)
                {
                    readIndex = 0;
                }
            }
        }
        else
        {
            handleValue(Value);
            handleBoolValue(IsPressed);
        }
    }

    protected virtual void handleValue(float val)
    {

    }

    protected virtual void handleBoolValue(bool val)
    {

    }


    public float Value
    {
        get;set;
    }

    public float Delay
    {
        get;set;
    }

    public bool IsPressed
    {
        get;set;
    }
}
