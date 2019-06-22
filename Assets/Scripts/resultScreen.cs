using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resultScreen : MonoBehaviour
{

    public TextMeshProUGUI moneyText, totalDamageText, DamageText, totalTimeText, timeText, rocketWorth;
    private float currentRocketWorth;
    private float damageFaktor = SavedGame.CurrentRocketGlobalDamage;
    private float timeFaktor = SavedGame.CurrentRocketGlobalTime;
    [SerializeField]
    private float worthFaktor;

    public void showEndScreen()
    {

        

        moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + timeFaktor.ToString();
        totalDamageText.text = "Total Damage: " + damageFaktor.ToString();

        currentRocketWorth = (worthFaktor / timeFaktor) - damageFaktor;

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString();

    }

    private void Start()
    {
        moneyText.text = SavedGame.Money.ToString() + "$";
        totalTimeText.text = "Total Time: " + timeFaktor.ToString();
        totalDamageText.text = "Total Damage: " + damageFaktor.ToString();

        currentRocketWorth = (worthFaktor / timeFaktor) - damageFaktor;

        rocketWorth.text = "Rocket Worth: " + currentRocketWorth.ToString();

    }

}
