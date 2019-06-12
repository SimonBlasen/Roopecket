using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopMultipleActivationDoor : CoopActivatable
{
    [SerializeField]
    private CoopMultipleActivationDoorDoor door;

    private bool wasPressed = false;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
        if (wasPressed == false && IsPressed)
        {
            wasPressed = true;
            door.Counter++;
        }
    }
}
