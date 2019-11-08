using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBlinkSrcManeuver : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private bool fadeIn = true;
    private bool shouldSafe = false;
    private bool deactivated = false;

    private float alpha = 0f;
    private float moversOutFor = 0f;
    private float moversInFor = 0f;

    private bool isNoIdiot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool checkedExtra = false;

    // Update is called once per frame
    void Update()
    {
        if (!checkedExtra)
        {
            checkedExtra = true;
            bool has = false;
            for (int i = 0; i < rocketController.transform.GetComponent<RocketProps>().usesExtraFuel.Length; i++)
            {
                if (rocketController.transform.GetComponent<RocketProps>().usesExtraFuel[i])
                {
                    has = true;
                    break;
                }
            }

            if (!has)
            {
                deactivated = true;
                shouldSafe = false;
            }
        }

        if (shouldSafe)
        {
            if (fadeIn)
            {
                alpha += Time.deltaTime;
                if (alpha >= 1f)
                {
                    fadeIn = false;
                    alpha = 1f;
                }
            }
            else
            {
                alpha -= Time.deltaTime;
                if (alpha <= 0f)
                {
                    if (deactivated == false)
                    {
                        fadeIn = true;
                    }
                    else
                    {
                        tmp.text = "";
                        shouldSafe = false;
                    }
                    alpha = 0f;
                }
            }

            if ((Input.GetKey(Statics.keySpecialLeft) || Input.GetKey(Statics.keySpecialRight)))
            {
                deactivated = true;
            }
        }

        if (!deactivated)
        {
            if (shouldSafe == false && (Input.GetKey(Statics.keySpecialLeft) || Input.GetKey(Statics.keySpecialRight)) == false && rocketController.IsThrusting())
            {
                moversOutFor += Time.deltaTime;
                if (isNoIdiot == false && moversOutFor >= 5f)
                {
                    shouldSafe = true;
                }
            }
            else if (isNoIdiot == false && (Input.GetKey(Statics.keySpecialLeft) || Input.GetKey(Statics.keySpecialRight)) && rocketController.IsThrusting())
            {
                moversInFor += Time.deltaTime;
                if (moversInFor >= 0.2f)
                {
                    isNoIdiot = true;
                    deactivated = true;
                    tmp.text = "";
                }
            }
        }

        

        tmp.color = new Color(1f, 1f, 1f, alpha);
    }

    public RocketController rocketController = null;
}
