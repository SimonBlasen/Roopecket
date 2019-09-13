using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysDisplay : MonoBehaviour
{
    public TextMeshProUGUI textAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private float counter = 0f;
    private int oldKeysAmount = -1;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 0.2f)
        {
            counter = 0f;
            if (oldKeysAmount != SavedGame.RocketUnlockKeys)
            {
                oldKeysAmount = SavedGame.RocketUnlockKeys;

                textAmount.text = oldKeysAmount.ToString();
            }
        }
    }
}
