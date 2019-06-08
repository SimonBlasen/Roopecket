using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopCable : MonoBehaviour {

    [SerializeField]
    private Transform[] lighting;
    [SerializeField]
    private Material matOn;
    [SerializeField]
    private Material matOff;
    [SerializeField]
    private bool delayedCable;
    [SerializeField]
    private float waitTime = 0.2f;

    private float waitTimeCounter = 0f;

    private bool switched = false;
    private int index = 0;

    // Use this for initialization
    void Start ()
    {
        SwitchedOn = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (switched && index < lighting.Length)
        {
            waitTimeCounter += Time.deltaTime;

            if (waitTimeCounter >= waitTime)
            {
                waitTimeCounter = 0f;
                
                lighting[index].GetComponent<MeshRenderer>().sharedMaterial = matOn;
                
                index++;
            }
        }
	}

    public bool SwitchedOn
    {
        get
        {
            return switched;
        }
        set
        {
            switched = value;

            if (switched == false)
            {
                for (int i = 0; i < lighting.Length; i++)
                {
                    lighting[i].GetComponent<MeshRenderer>().sharedMaterial = matOff;
                }
            }
            else
            {
                index = 0;
                waitTimeCounter = 0f;
            }
        }
    }
}
