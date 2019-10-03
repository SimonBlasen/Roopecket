using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PvPWonTextAnim : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private TextMeshProUGUI textPressKey;
    [SerializeField]
    private float textSpeedInc = 1f;

    private float endFontSize = 100f;
    private float s = 0f;

    private bool anim = false;

    // Start is called before the first frame update
    void Start()
    {
        endFontSize = text.fontSize;
        StopAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            s += Time.deltaTime * textSpeedInc;


            text.fontSize = 100 * (s > 1f ? 1f : s);

            if (s >= 4f)
            {
                textPressKey.enabled = true;
            }
        }
    }

    public void StartAnim(int playerWon)
    {
        anim = true;
        text.fontSize = 0;
        text.text = "Player " + playerWon + " won";
        s = 0f;
    }

    public void StopAnim()
    {
        textPressKey.enabled = false;
        text.text = "";
    }
}
