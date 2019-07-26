using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelTime : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI textHunds;
    private RocketProps rocketProps;
    private RocketSpawner rs;
    private TimeKeeper tk = null;
    public float text1BeginSize = 39.3f;
    public float text2BeginSize = 25.4f;
    public float text1GoalSize = 35f;
    public float text2GoalSize = 20f;

    public float text1BeginX;
    public float text2BeginX;
    public float text1GoalX;
    public float text2GoalX;

    private int oldSeconds = -1;

    private float lerpStep = 0f;
    private bool lerping = false;

    // Start is called before the first frame update
    void Start()
    {
        rocketProps = null;
        rs = GameObject.FindObjectOfType<RocketSpawner>();
        tk = GameObject.FindObjectOfType<TimeKeeper>();

        text.fontSize = text1BeginSize;
        textHunds.fontSize = text2BeginSize;
        text.rectTransform.localPosition = new Vector3(text1BeginX, text.rectTransform.localPosition.y, text.rectTransform.localPosition.z);
        textHunds.rectTransform.localPosition = new Vector3(text2BeginX, text.rectTransform.localPosition.y, text.rectTransform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketProps == null)
        {
            if (rs != null)
            {
                if (rs.SpawnedRocket != null)
                {
                    rocketProps = rs.SpawnedRocket.GetComponent<RocketProps>();
                }
            }
            else
            {
                rocketProps = GameObject.FindObjectOfType<RocketProps>();
            }
        }
        else
        {
            float time = tk.GetCurrentTime();

            int seconds = (int)time;
            int hundsSeconds = (int)((time - seconds) * 100f);

            //text.text = seconds.ToString() + ":" +  (hundsSeconds < 10 ? "0" : "") + hundsSeconds.ToString();

            textHunds.text = (hundsSeconds < 10 ? "0" : "") + hundsSeconds.ToString();

            if (oldSeconds != seconds)
            {
                oldSeconds = seconds;
                text.text = seconds.ToString();

                if (seconds == 99)
                {
                    lerpStep = 0f;
                    lerping = true;
                }
            }

        }

        if (lerping)
        {
            lerpStep += Time.deltaTime;
            if (lerpStep > 1f)
            {
                lerpStep = 1f;
                lerping = false;
            }
            text.fontSize = (text1GoalSize - 39.3f) * lerpStep + 39.3f;
            textHunds.fontSize = (text2GoalSize - 25.4f) * lerpStep + 25.4f;

            text.rectTransform.localPosition = new Vector3((text1GoalX - text1BeginX) * lerpStep + text1BeginX, text.rectTransform.localPosition.y, text.rectTransform.localPosition.z);
            textHunds.rectTransform.localPosition = new Vector3((text2BeginX - text2GoalX) * lerpStep + text2BeginX, textHunds.rectTransform.localPosition.y, textHunds.rectTransform.localPosition.z);
        }
    }
}
