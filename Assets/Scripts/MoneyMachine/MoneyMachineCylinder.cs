using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MMCylState
{
    STOP, ACC, FULL_SPEED, FS_WAIT, DEC, REV
}

public class MoneyMachineCylinder : MonoBehaviour
{
    private MoneyMachineNumber[] numbers;

    [SerializeField]
    private float maxVelocity = 20f;
    [SerializeField]
    private float acceleration = 1f;
    [SerializeField]
    private float decceleration = 1f;
    [SerializeField]
    private float turnAngleOffset = 0f;
    [SerializeField]
    private float timeOnFullspeed = 0f;
    //[SerializeField]
    //private int timeStepsToDec = 60;

    private MMCylState state = MMCylState.STOP;

    private float currentVel = 0f;
    private float timeFSCounter = 0f;

    private float assumedTimeDelta = 1f / 60f;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShownNumber++;
            Debug.Log("Show number " + ShownNumber.ToString());
        }

        if (state == MMCylState.ACC || state == MMCylState.FS_WAIT || state == MMCylState.FULL_SPEED)
        {
            transform.Rotate(0f, currentVel, 0f, Space.Self);
        }
        else
        {
            transform.Rotate(0f, currentVel, 0f, Space.Self);
        }


        if (state == MMCylState.ACC)
        {
            if (currentVel < maxVelocity)
            {
                currentVel += acceleration;
                if (currentVel > maxVelocity)
                {
                    currentVel = maxVelocity;
                }
            }
            else
            {
                state = MMCylState.FULL_SPEED;
                Debug.Log(state);
                timeFSCounter = 0f;
            }
        }
        else if (state == MMCylState.FULL_SPEED)
        {
            timeFSCounter += Time.deltaTime;
            if (timeFSCounter >= timeOnFullspeed)
            {
                state = MMCylState.FS_WAIT;
                Debug.Log(state);
                angleX0 = calcX0(targetAngle) % 360f;
                while (angleX0 < 0f)
                {
                    angleX0 += 360f;
                }
                oldDistanceWait = 360f;
                Debug.Log("Angle x0: " + angleX0.ToString() + ", Dest: " + targetAngle.ToString());
            }
        }
        else if (state == MMCylState.FS_WAIT)
        {
            float a1 = Vector3.Angle(transform.forward, new Vector3(0f, 0f, 1f));
            float a2 = Vector3.Angle(transform.forward, new Vector3(0f, 1f, 0f));
            if (a2 < 90f)
            {
                a1 = 360f - a1;
            }
            //Debug.Log(a1);
            if (getAngleDistance(a1, angleX0) > oldDistanceWait && oldDistanceWait < 10f)
            {
                transform.localRotation = Quaternion.Euler(angleX0, 0f, 90f);
                state = MMCylState.DEC;
                Debug.Log(state);
                Debug.Log("A1: " + a1.ToString());
            }
            else
            {
                oldDistanceWait = getAngleDistance(a1, angleX0);
            }
            
        }
        else if (state == MMCylState.DEC)
        {
            currentVel -= decceleration;
            if (currentVel < 0f)
            {
                state = MMCylState.STOP;
                Debug.Log(state);
                currentVel = 0f;
            }
        }
    }

    private float angleX0 = 0f;
    private float oldDistanceWait = 360f;

    private float getAngleDistance(float a1, float a2)
    {
        float an1 = a1 % 360f;
        float an2 = a2 % 360f;
        while (an2 < 0f)
        {
            an2 += 360f;
        }
        while (an1 < 0f)
        {
            an1 += 360f;
        }

        if (Mathf.Abs(an1 - an2) > 180f)
        {
            return 360f - Mathf.Abs(an1 - an2);
        }
        else
        {
            return Mathf.Abs(an1 - an2);
        }
    }

    private float calcX0(float xDest)
    {
        //float dec = maxVelocity / (timeStepsToDec * decceleration);
        //return 0.5f * timeStepsToDec * timeStepsToDec * dec - maxVelocity * timeStepsToDec;


        //float otherTerm = ((maxVelocity * maxVelocity) / decceleration) - 0.5f * ((maxVelocity * maxVelocity) / decceleration);

        float otherTerm = 0.5f * ((maxVelocity * maxVelocity) / decceleration);

        while (otherTerm > xDest)
        {
            xDest += 360f;
        }

        return xDest - otherTerm;

        //return xDest - (0.5f * ((maxVelocity * maxVelocity) / decceleration));
    }

    private bool inited = false;
    private void init()
    {
        if (!inited)
        {
            inited = true;

            MoneyMachineNumber[] foundNumbers = GetComponentsInChildren<MoneyMachineNumber>();

            numbers = new MoneyMachineNumber[foundNumbers.Length];

            if (foundNumbers.Length != 10)
            {
                Debug.LogError("Money Machine numbers is not 10");
            }

            for (int i = 0; i < foundNumbers.Length; i++)
            {
                numbers[foundNumbers[i].RepNumber] = foundNumbers[i];
            }


        }
    }

    private int toShowNumber = 0;
    public int ShownNumber
    {
        get
        {
            return toShowNumber;
        }
        set
        {
            init();
            toShowNumber = value;

            state = MMCylState.ACC;
            Debug.Log(state);
        }
    }

    private float targetAngle
    {
        get
        {
            return toShowNumber * 36f + turnAngleOffset;
        }
    }
}
