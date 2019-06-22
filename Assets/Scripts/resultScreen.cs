using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultScreen : MonoBehaviour
{

    public TextMeshProUGUI moneyText, totalDamageText, DamageText, totalTimeText, timeText, rocketWorth;
    private float currentRocketWorth;
    private float damageFaktor = 1f;
    private float timeFaktor = 1f;
    private float worthFaktor = 1200f;

    private void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void showEndScreen()
    {
        GetComponent<Canvas>().enabled = true;
        

        moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + SavedGame.CurrentRocketGlobalTime.ToString("#.###");
        totalDamageText.text = "Total Damage: " + SavedGame.CurrentRocketGlobalDamage.ToString();

        currentRocketWorth = worthFaktor / (SavedGame.CurrentRocketGlobalTime * (SavedGame.CurrentRocketGlobalDamage + 0.01f));

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString("#.##");

    }

    public void ButtonContinueClick()
    {
        SceneManager.LoadScene(Statics.nextScene);
    }

    /*private void Start()
    {
        moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + timeFaktor.ToString();
        totalDamageText.text = "Total Damage: " + damageFaktor.ToString();

        currentRocketWorth = (worthFaktor / timeFaktor) - damageFaktor;

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString();

    }*/

}
