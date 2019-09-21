using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBlinkScr : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
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

            if (rocketController.LandingMoversOut == false)
            {
                deactivated = true;
            }
        }

        if (!deactivated)
        {
            if (shouldSafe == false && rocketController.LandingMoversOut && rocketController.IsThrusting())
            {
                moversOutFor += Time.deltaTime;
                if (isNoIdiot == false && moversOutFor >= 4f)
                {
                    shouldSafe = true;
                }
            }
            else if (isNoIdiot == false && rocketController.LandingMoversOut == false && rocketController.IsThrusting())
            {
                moversInFor += Time.deltaTime;
                if (moversInFor >= 7f)
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
