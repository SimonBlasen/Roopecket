using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopDoor : CoopActivatable
{
    [SerializeField]
    private Transform doorObject;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;



    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    new void Update ()
    {
        base.Update();
    }

    protected override void handleValue(float val)
    {
        doorObject.localPosition = Vector3.Lerp(doorObject.localPosition, new Vector3(doorObject.localPosition.x, yMin + val * (yMax - yMin), doorObject.localPosition.z), 0.03f);
    }


}
