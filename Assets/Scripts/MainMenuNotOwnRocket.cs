using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuNotOwnRocket : MonoBehaviour
{
    public MenuPlanet[] menuPlanets;

    // Start is called before the first frame update
    void Start()
    {
        if (SavedGame.LastPlayedRocket == -1)
        {
            GetComponent<TextMeshPro>().enabled = true;
            for (int i = 0; i < menuPlanets.Length; i++)
            {
                menuPlanets[i].Disable();
            }
        }
        else
        {
            GetComponent<TextMeshPro>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
