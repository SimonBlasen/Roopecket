using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopMultipleActivationDoorDoor : MonoBehaviour
{
    [SerializeField]
    private CoopActivatable[] activatables;
    [SerializeField]
    private int amountsToActivate = 1;

    private bool wasActivated = false;

	// Use this for initialization
	void Start ()
    {
        Counter = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (wasActivated == false && Counter >= amountsToActivate)
        {
            wasActivated = true;
            for (int i = 0; i < activatables.Length; i++)
            {
                activatables[i].IsPressed = true;
            }
        }
    }

    public int Counter
    {
        get; set;
    }
}
