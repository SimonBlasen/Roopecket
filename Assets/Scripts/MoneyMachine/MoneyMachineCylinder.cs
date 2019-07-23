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
    [Space]

    public int n0;

    [SerializeField]
    private float aT = 2f;
    [SerializeField]
    private float dT = 3f;
    [SerializeField]
    private float fT = 5f;
    //[SerializeField]
    //private int timeStepsToDec = 60;

    private MMCylState state = MMCylState.STOP;

    private float currentVel = 0f;
    private float timeFSCounter = 0f;

    private float assumedTimeDelta = 1f / 60f;


    float xDest = 0f;
    float startOffset = 0f;
    float curT = 0f;

    private float xT(float t)
    {
        if (t < aT)
        {
            return 0.5f * acceleration * t * t;
        }
        /*else if (t < dT)
        {
            return acceleration * aT * t + 0.5f * acceleration * aT * aT;
        }*/
        else if (t <= fT)
        {
            t = t - aT;
            return -acceleration * 0.5f * t * t + aT * 1f * acceleration * t + 0.5f * acceleration * aT * aT;
        }
        else
        {
            return xDest;
            return 0.5f * acceleration * aT * aT + acceleration * aT * dT - acceleration * aT * aT - 0.5f * acceleration * fT * fT + 0.5f * acceleration * dT * dT;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private float xA = 0f;
    private bool moving = false;
    private bool animating = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Start moving to " + targetAngle);
        }

        if (moving)
        {
            transform.localRotation = Quaternion.Euler(xT(curT) + startOffset, 0f, 90f);
            curT += Time.deltaTime;
            if (curT > fT + 0.2f)
            {
                animating = false;

                if (bufferNumbers.Count > 0)
                {
                    ShownNumber = bufferNumbers[bufferNumbers.Count - 1];
                    bufferNumbers.Clear();
                    bufferNumbers = new List<int>();
                }
                //moving = false;
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

    private List<int> bufferNumbers = new List<int>();

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

            if (!animating)
            {
                startOffset = targetAngle;
                toShowNumber = value;
                animating = true;



                //ShownNumber = n0;
                //Debug.Log("Show number " + ShownNumber.ToString());

                int turnArounds = Random.Range(1, 4);

                xDest = targetAngle + 360f * turnArounds - startOffset;
                curT = 0f;
                dT = aT;
                decceleration = acceleration;
                fT = 2f * aT;
                acceleration = (xDest) / (aT * aT);
                moving = true;

            }
            else
            {
                bufferNumbers.Add(value);
            }
            //state = MMCylState.ACC;
            //Debug.Log(state);
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
