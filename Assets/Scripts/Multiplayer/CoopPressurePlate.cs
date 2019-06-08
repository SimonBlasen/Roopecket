using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PressurePlateType
{
    TACTILE = 0, BUTTON = 1, WEIGHTED = 2
}


public class CoopPressurePlate : MonoBehaviour
{
    [SerializeField]
    private CoopActivatable activatable;
    [SerializeField]
    private CoopCable cable;
    [SerializeField]
    private Transform child;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private PressurePlateType type;
    [SerializeField]
    private float maxSpeed = 0f;
    [SerializeField]
    private float brakeMultiplier = 1f;
    [SerializeField]
    private float activationDelay = 1f;



    private bool buttonIsPressed = false;
    private Rigidbody rigChild;

    // Use this for initialization
    void Start ()
    {
        rigChild = child.GetComponent<Rigidbody>();

        if (activatable != null)
        {
            activatable.Delay = activationDelay;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Pressure: " + PressedAmount.ToString());

        if (type == PressurePlateType.BUTTON && buttonIsPressed == false && IsPressed)
        {
            buttonIsPressed = true;
            //Transform springTrans = GetComponentInChildren<SpringJoint>().transform;
            Destroy(GetComponentInChildren<SpringJoint>());
            Debug.Log("Destroyed spring");
        }

        if (type == PressurePlateType.WEIGHTED)
        {
            if (Mathf.Abs(rigChild.velocity.y) > maxSpeed)
            {
                rigChild.AddForce(Vector3.down * brakeMultiplier * Mathf.Sign(rigChild.velocity.y) * (Mathf.Abs(rigChild.velocity.y) - maxSpeed));

                //rigChild.velocity = new Vector3(0f, Mathf.Sign(rigChild.velocity.y) * maxSpeed);

                //Debug.Log(rigChild.velocity.y);
            }

            if (child.localPosition.y > yMax + 0.06f)
            {
                child.localPosition = new Vector3(child.localPosition.x, yMax, child.localPosition.z);
            }
            else if (child.localPosition.y < yMin - 0.06f)
            {
                child.localPosition = new Vector3(child.localPosition.x, yMin, child.localPosition.z);
            }

        }


        if (activatable != null)
        {
            activatable.Value = PressedAmount;
        }

        if (cable != null)
        {
            if ((PressedAmount >= 0.05f) != cable.SwitchedOn)
            {
                cable.SwitchedOn = (PressedAmount >= 0.05f);
            }
        }

    }

    public float PressedAmount
    {
        get
        {
            return (yMax - child.localPosition.y) / (yMax - yMin);
        }
    }

    public bool IsPressed
    {
        get
        {
            return PressedAmount >= 0.8f;
        }
    }
}
