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
    private CoopActivatable[] activatables;
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
    private float[] activationDelay;
    [SerializeField]
    private bool useColorPressed = false;
    [SerializeField]
    private Color colorPressed;


    private MeshRenderer[] colorMeshs = new MeshRenderer[2];
    private Color colorReleased;
    private Material clonedMat;

    private bool buttonIsPressed = false;
    private Rigidbody rigChild;

    // Use this for initialization
    void Start ()
    {
        rigChild = child.GetComponent<Rigidbody>();
        colorReleased = child.GetComponent<MeshRenderer>().sharedMaterial.color;
        clonedMat = new Material(child.GetComponent<MeshRenderer>().sharedMaterial);
        colorMeshs[0] = child.GetComponent<MeshRenderer>();
        colorMeshs[1] = child.GetComponentInChildren<MeshRenderer>();
        colorMeshs[0].sharedMaterial = clonedMat;
        colorMeshs[1].sharedMaterial = clonedMat;

        if (activatables.Length > 0)
        {
            for (int i = 0; i < activatables.Length; i++)
            {
                activatables[i].Delay = activationDelay[i];
            }
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

        if (useColorPressed)
        {
            for (int i = 0; i < colorMeshs.Length; i++)
            {
                colorMeshs[i].sharedMaterial.color = Color.Lerp(colorReleased, colorPressed, Mathf.Min(1f, Mathf.Max(0f, PressedAmount)));
            }
        }


        if (activatables.Length > 0)
        {
            for (int i = 0; i < activatables.Length; i++)
            {
                activatables[i].Value = PressedAmount;
                activatables[i].IsPressed = IsPressed;
            }
        }

        if (cable != null)
        {
            if (IsPressed != cable.SwitchedOn)
            {
                cable.SwitchedOn = IsPressed;
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
            return PressedAmount >= 0.05f;
        }
    }
}
