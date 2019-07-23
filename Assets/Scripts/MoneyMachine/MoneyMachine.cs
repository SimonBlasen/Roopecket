using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMachine : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private int startNumber = 0;
    [SerializeField]
    private float delayBetweenCylinders = 0.1f;


    [Header("References")]
    [SerializeField]
    private MoneyMachineCylinder[] cylinders;

    private int nextCylinderAnimate = 0;
    private float timeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (currentNumber == -1)
        {
            Number = startNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextCylinderAnimate < cylinders.Length)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= delayBetweenCylinders)
            {
                timeCounter = 0f;

                cylinders[nextCylinderAnimate].ShownNumber = cropNumber % 10;
                cropNumber = cropNumber / 10;

                nextCylinderAnimate++;
            }
        }
    }

    private int cropNumber = 0;

    private int currentNumber = -1;
    public int Number
    {
        get
        {
            return currentNumber;
        }
        set
        {
            if (value != currentNumber)
            {
                currentNumber = value;

                timeCounter = 0f;

                cropNumber = currentNumber;

                nextCylinderAnimate = 0;
            }
        }
    }
}
