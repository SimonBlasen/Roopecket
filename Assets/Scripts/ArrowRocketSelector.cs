using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrowRocketSelector : MonoBehaviour
{

    public bool mouseHover = false;
    public bool mousedown = false;
    private bool locked = false;

    public float left = 0;
    public float right = 0;
    public int rocketNubmber = 1;
    public GarageRocketnameTurner rocketNameTurner;

    public Transform[] rockets;
    public Transform currentRocket;

    public GameObject cage;
    public GameObject prefabRocketnameCanvas;
    public GameObject prefabTextTooShort;
    private GameObject instRocketnameCanvas;
    private TMP_InputField inputRocketName;
    public GarageArrows[] arrows;

    Vector3 spawn;
    Vector3 selected;

    private int selectedBuyRocket = -1;

    public Vector3 rocketSpawn;

    private bool mouseIsDown = false;

    private void Start()
    {

        SavedGame.Money = 100000;
        instRocketnameCanvas = Instantiate(prefabRocketnameCanvas);
        instRocketnameCanvas.GetComponent<Canvas>().enabled = false;
        inputRocketName = instRocketnameCanvas.GetComponentInChildren<TMP_InputField>();
        instRocketnameCanvas.GetComponentInChildren<Button>().onClick.AddListener(ConfirmRocketNameClick);

        rocketNubmber = Statics.selectedRocket;
        spawn = transform.position;
        selected = spawn + new Vector3(0f, 0f, -0.2f);

        for (int i = 0; i < rockets.Length; i++)
        {
            rockets[i].gameObject.SetActive(false);
        }

        currentRocket = rockets[rocketNubmber];
        currentRocket.gameObject.SetActive(true);
        currentRocket.transform.position = rocketSpawn;
        currentRocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentRocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        SavedGame.OwnedRockets[0] = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputRocketName.isFocused)
        {
            ConfirmRocketNameClick();

        }
    }

    private void setRocketActive(int number)
    {
        selectedBuyRocket = number;
        if (number >= 0 && number < rockets.Length)
        {
            Statics.selectedRocket = number;

            currentRocket.gameObject.SetActive(false);

            currentRocket = rockets[number];

            currentRocket.gameObject.SetActive(true);
            currentRocket.transform.position = rocketSpawn;
            currentRocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentRocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            
            //cage.SetActive(SavedGame.OwnedRockets[number] != -1);
            
            rocketNameTurner.ShowRocketName("Price: " + SavedGame.RocketPrices[number]);

            if (SavedGame.Money >= SavedGame.RocketPrices[number])
            {
                //TODO enable buy button
            }
            else
            {
                //TODO disable buy button
            }
        }
    }

    public void BuyRocketClick()
    {
        if (SavedGame.Money >= SavedGame.RocketPrices[selectedBuyRocket])
        {
            //TODO Buy rocket
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].KeysLocked = true;
            }
            instRocketnameCanvas.GetComponent<Canvas>().enabled = true;
            inputRocketName.text = "";
        }
    }

    public void ConfirmRocketNameClick()
    {
        if (inputRocketName.text.Length > 0)
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].KeysLocked = false;
            }
            SavedGame.Money -= SavedGame.RocketPrices[selectedBuyRocket];
            //SavedGame.RocketNames[selectedBuyRocket] = inputRocketName.text;
            for (int i = 0; i < SavedGame.OwnedRockets.Length; i++)
            {
                if (SavedGame.OwnedRockets[i] != -1)
                {
                    SavedGame.OwnedRockets[i] = selectedBuyRocket;
                    SavedGame.RocketNames[i] = inputRocketName.text;
                    break;
                }
            }

            inputRocketName.text = "";
            instRocketnameCanvas.GetComponent<Canvas>().enabled = false;

            //TODO Show rocket bought
        }
        else
        {
            GameObject instT = Instantiate(prefabTextTooShort);
            Destroy(instT, 4f);
        }
    }
    
    public void LeftClick()
    {
        if (rocketNubmber <= 0)
            rocketNubmber = rockets.Length - 1;

        else
            rocketNubmber -= 1;

        print(rocketNubmber);
        setRocketActive(rocketNubmber);
    }

    public void RightClick()
    {
        if (rocketNubmber >= rockets.Length - 1)
            rocketNubmber = 0;

        else
            rocketNubmber += 1;

        print(rocketNubmber);
        setRocketActive(rocketNubmber);
    }
}
