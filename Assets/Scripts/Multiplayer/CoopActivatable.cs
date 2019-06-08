using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopActivatable : MonoBehaviour
{

    private float[] times = new float[4048];
    private float[] values = new float[4048];

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
            writeIndex++;
            if (writeIndex >= times.Length)
            {
                writeIndex = 0;
            }

            if (Time.time > times[readIndex])
            {
                handleValue(values[readIndex]);
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
        }
    }

    protected virtual void handleValue(float val)
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
}
