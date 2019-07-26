using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevelInfo : MonoBehaviour
{
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    public GameObject panelWorldTarget = null;
    [SerializeField]
    public Vector2 offset;
    [SerializeField]
    private TextMeshProUGUI levelName;
    [SerializeField]
    private TextMeshProUGUI levelTime;
    [SerializeField]
    private TextMeshProUGUI levelDamage;
    [SerializeField]
    private TextMeshProUGUI levelFuel;
    [SerializeField]
    private TextMeshProUGUI levelWorthness;
    [SerializeField]
    private Button buttonContinue;
    [SerializeField]
    private Button buttonFreestyle;

    RectTransform canvasRect;
    Canvas selfCanvas;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        selfCanvas = GetComponent<Canvas>();
        selfCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (panelWorldTarget != null)
        {
            if (selfCanvas.enabled == false)
            {
                selfCanvas.enabled = true;
            }
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(panelWorldTarget.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            panel.anchoredPosition = WorldObject_ScreenPosition + offset;
        }
        else
        {
            if (selfCanvas.enabled)
            {
                selfCanvas.enabled = false;
            }
        }
    }

    public void ButtonContinueClick()
    {
        Statics.isInFreestyle = false;
        SceneManager.LoadScene(StaticsSingleplayer.GetSceneToLoad(StaticsSingleplayer.levelNames[LevelIndex]));
    }

    public void ButtonFreestyleClick()
    {
        Statics.isInFreestyle = true;
        SceneManager.LoadScene(StaticsSingleplayer.levelNames[LevelIndex]);
    }

    public bool IsLevelMastered(int level)
    {
        return level < SavedGame.NextLevel[SavedGame.LastPlayedRocket];
    }

    private int levelIndex = -1;
    public int LevelIndex
    {
        get
        {
            return levelIndex;
        }
        set
        {
            levelIndex = value;

            if (levelIndex == SavedGame.NextLevel[SavedGame.LastPlayedRocket])
            {
                buttonContinue.interactable = true;
            }
            else
            {
                buttonContinue.interactable = false;
            }

            if (levelIndex < SavedGame.NextLevel[SavedGame.LastPlayedRocket])
            {
                buttonFreestyle.interactable = true;
            }
            else
            {
                buttonFreestyle.interactable = false;
            }


            float time = SavedGame.CurrentTimeStage[SavedGame.LastPlayedRocket, levelIndex];
            float damage = SavedGame.CurrentDamageStage[SavedGame.LastPlayedRocket, levelIndex];
            float fuel = SavedGame.CurrentUsedFuel[SavedGame.LastPlayedRocket, levelIndex];
            float worthness = resultScreen.CalculateRocketWorth(time, damage, fuel, 1);

            levelName.text = "Level " + (levelIndex + 1).ToString();
            levelTime.text = time.ToString("n3");
            levelDamage.text = damage.ToString("n0");
            levelFuel.text = fuel.ToString("n2");
            levelWorthness.text = worthness.ToString("n2");
        }
    }
}
