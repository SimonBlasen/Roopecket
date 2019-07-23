using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyMachineNumber : MonoBehaviour
{
    private TextMeshPro numberText;


    private float colorInterpolSpeed = 0.5f;

    private float lerpStep = 0.0f;

    private bool changeColor = false;

    private bool inited = false;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        if (!inited)
        {
            numberText = GetComponent<TextMeshPro>();
            inited = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Glow && lerpStep < 1f)
        {
            lerpStep += colorInterpolSpeed * Time.deltaTime;
            if (lerpStep > 1f)
            {
                lerpStep = 1f;
            }
        }
        else if (Glow == false && lerpStep > 0f)
        {
            lerpStep -= colorInterpolSpeed * Time.deltaTime;
            if (lerpStep < 0f)
            {
                lerpStep = 0f;
            }
        }

        if (changeColor)
        {
            numberText.color = Color.Lerp(ColorInactive, ColorActive, lerpStep);
        }

        if (changeColor && lerpStep == 0f || lerpStep == 1f)
        {
            changeColor = false;
        }
    }

    public Color ColorActive
    {
        get;set;
    }

    public Color ColorInactive
    {
        get;set;
    }

    private bool glowing = false;

    public bool Glow
    {
        get
        {
            return glowing;
        }
        set
        {
            init();
            glowing = value;
            changeColor = true;
        }
    }

    public int RepNumber
    {
        get
        {
            return System.Convert.ToInt32(numberText.text);
        }
    }
}
