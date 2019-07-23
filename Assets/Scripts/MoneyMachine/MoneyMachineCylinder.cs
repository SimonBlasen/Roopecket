using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMachineCylinder : MonoBehaviour
{
    private MoneyMachineNumber[] numbers;

    private HingeJoint hingeJoint;

    // Start is called before the first frame update
    void Start()
    {
        init();
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



            hingeJoint = GetComponent<HingeJoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
