using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRocketGreenFill : MonoBehaviour
{

    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    private RectTransform insideRocket;
    public RocketProps rocketProps;
    public bool secondPlayer = false;

    private float healthPerc = 0f;

    private float maxHeight = 0f;

    private int lastHealth = 0;

    RocketSpawner rs;

    // Use this for initialization
    void Start ()
    {
        maxHeight = insideRocket.sizeDelta.y;

        //rocketProps = GameObject.FindObjectOfType<RocketProps>();
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rocketProps == null)
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
            if (rocketProps.CurrentHealth != lastHealth)
            {
                lastHealth = rocketProps.CurrentHealth;
                healthPercentage = ((float)lastHealth) / rocketProps.MaxHealth;
            }
        }
    }

    public float healthPercentage
    {
        get
        {
            return healthPerc;
        }
        set
        {
            healthPerc = value;

            insideRocket.localPosition = new Vector3(0f, (1f - healthPerc) * maxHeight, 0f);

            panel.localPosition = new Vector3(0f, -(1f - healthPerc) * maxHeight, 0f);

            insideRocket.GetComponent<Image>().color = new Color(1f, healthPerc, healthPerc);
        }
    }
}
