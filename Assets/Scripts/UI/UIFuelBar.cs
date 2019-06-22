﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFuelBar : MonoBehaviour {

    public Image image;
    public TextMeshProUGUI text;
    public RocketProps rocketProps;
    public bool secondPlayer = false;

    private float lastFuel = 0f;

    RocketSpawner rs;

    // Use this for initialization
    void Start()
    {
        //rocketProps = GameObject.FindObjectOfType<RocketProps>();
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketProps == null)
        {
            if (rs != null)
            {
                if (secondPlayer == false && rs.SpawnedRocket != null)
                {
                    rocketProps = rs.SpawnedRocket.GetComponent<RocketProps>();
                }
                else if (secondPlayer == true && rs.SpawnedRocket2 != null)
                {
                    rocketProps = rs.SpawnedRocket2.GetComponent<RocketProps>();
                }
            }
            else
            {
                rocketProps = GameObject.FindObjectOfType<RocketProps>();
            }
        }
        else
        {
            if (rocketProps.CurrentFuel != lastFuel)
            {
                lastFuel = rocketProps.CurrentFuel;
                float percent = lastFuel / rocketProps.MaxFuel;
                image.fillAmount = percent;
                int percentInt = (int)(percent * 100f + 0.999f);
                text.text = percentInt.ToString() + "%";
            }
        }
    }
}
