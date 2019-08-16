using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTutorial11 : MonoBehaviour
{
    [SerializeField]
    public RocketController rocketController;
    [SerializeField]
    private TutorialEndscreen tutorialEndscreen;

    private bool didTakeThemIn = false;

    private string oldText = "";
    private string oldTextTitle = "";

    // Start is called before the first frame update
    void Start()
    {
        oldText = tutorialEndscreen.ShowText;
        oldTextTitle = tutorialEndscreen.TitleText;

        tutorialEndscreen.ShowText = LanguageManager.Translate("Are you sure your closed you landing movers?") + "\n" + LanguageManager.Translate("Maybe try it once again");
        tutorialEndscreen.TitleText = LanguageManager.Translate("Nearly");
    }

    // Update is called once per frame
    void Update()
    {
        if (didTakeThemIn == false && rocketController.LandingMoversOut == false)
        {
            didTakeThemIn = true;
            tutorialEndscreen.ShowText = oldText;
            tutorialEndscreen.TitleText = oldTextTitle;
        }
    }
}
