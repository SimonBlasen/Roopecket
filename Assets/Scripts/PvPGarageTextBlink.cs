using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PvPGarageTextBlink : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textBlink;

    private float alpha = 0f;
    private bool fadeIn = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            alpha += Time.deltaTime;
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                fadeIn = false;
            }
        }
        else
        {
            alpha -= Time.deltaTime;
            if (alpha <= 0.3f)
            {
                alpha = 0.3f;
                fadeIn = true;
            }
        }

        textBlink.color = new Color(100f / 255f, 1f, 0f, alpha);
    }
}
