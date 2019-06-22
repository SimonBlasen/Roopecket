using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resultScreen : MonoBehaviour
{

    public TextMeshProUGUI moneyText, totalDamageText, DamageText, totalTimeText, timeText;

    public void showEndScreen()
    {

        moneyText.text = SavedGame.Money.ToString();
        totalTimeText.text = SavedGame.CurrentLevelTime.ToString();

    }

}
