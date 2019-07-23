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
    public Transform[] rocketsBought;

    public GameObject cage;
    public GameObject prefabRocketnameCanvas;
    public GameObject prefabTextTooShort;
    private GameObject instRocketnameCanvas;
    private TMP_InputField inputRocketName;
    public GarageArrows[] arrows;
    public GarageCameraLook garageCameraLook;
    public Button buttonBuyRocket;

    public MoneyMachine moneyMachine;

    Vector3 spawn;
    Vector3 selected;

    private int selectedBuyRocket = -1;

    public Vector3 rocketSpawn;
    public Transform rocketBoughtSpawn;

    private bool mouseIsDown = false;

    private void Start()
    {
        //
        //
        //TODO remove again

        //SavedGame.FillWithInitValues();

        //SavedGame.Money = 2000;

        // TODO
        //
        //


        moneyMachine.Number = SavedGame.Money;



        instRocketnameCanvas = Instantiate(prefabRocketnameCanvas);
        instRocketnameCanvas.GetComponent<Canvas>().enabled = false;
        inputRocketName = instRocketnameCanvas.GetComponentInChildren<TMP_InputField>();
        Button[] buttons = instRocketnameCanvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.name == "Button Confirm")
            {
                buttons[i].onClick.AddListener(ConfirmRocketNameClick);
            }
            else if (buttons[i].gameObject.name == "Button Abort")
            {
                buttons[i].onClick.AddListener(AbortBuyClick);
            }
        }



        spawn = transform.position;
        selected = spawn + new Vector3(0f, 0f, -0.2f);
        for (int i = 0; i < rockets.Length; i++)
        {
            rockets[i].gameObject.SetActive(false);
        }


        for (int i = 0; i < rocketsBought.Length; i++)
        {
            rocketsBought[i].gameObject.SetActive(false);
        }

        /*rocketNubmber = Statics.selectedRocket;


        currentRocket = rockets[rocketNubmber];
        currentRocket.gameObject.SetActive(true);
        currentRocket.transform.position = rocketSpawn;
        currentRocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentRocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        SavedGame.OwnedRockets[0] = 0;*/

        setRocketActive(0);
        setRocketBoughtActive(SavedGame.OwnedRockets[Statics.selectedRocket]);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputRocketName.isFocused)
        {
            ConfirmRocketNameClick();

        }
    }

    private int selectedBoughtRocket = 0;

    private void setRocketBoughtActive(int index)
    {
        if (index >= 0 && index < rockets.Length)
        {
            selectedBoughtRocket = index;
            int rocketType = SavedGame.OwnedRockets[selectedBoughtRocket];

            Statics.selectedRocket = rocketType;

            for (int i = 0; i < rocketsBought.Length; i++)
            {
                if (i == rocketType)
                {
                    rocketsBought[i].gameObject.SetActive(true);

                    rocketsBought[i].position = rocketBoughtSpawn.position;
                    rocketsBought[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    rocketsBought[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
                else
                {
                    rocketsBought[i].gameObject.SetActive(false);
                }
            }
            rocketNameTurner.ShowRocketName(SavedGame.RocketNames[selectedBoughtRocket]);

        }

    }

    private void setRocketActive(int number)
    {
        selectedBuyRocket = number;
        if (number >= 0 && number < rockets.Length)
        {
            //Statics.selectedRocket = number;

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
                buttonBuyRocket.interactable = true;
            }
            else
            {
                buttonBuyRocket.interactable = false;
            }
        }
    }

    public void BuyRocketClick()
    {
        if (SavedGame.Money >= SavedGame.RocketPrices[selectedBuyRocket])
        {
            moneyMachine.Number = SavedGame.Money - SavedGame.RocketPrices[selectedBuyRocket];
            //TODO Buy rocket
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].KeysLocked = true;
            }
            instRocketnameCanvas.GetComponent<Canvas>().enabled = true;
            inputRocketName.text = "";
        }
    }

    public void AbortBuyClick()
    {
        Debug.Log("Abort buy click");
        instRocketnameCanvas.GetComponent<Canvas>().enabled = false;
        moneyMachine.Number = SavedGame.Money;

        setRocketActive(selectedBuyRocket);
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].KeysLocked = false;
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

            int newRocketIndex = -1;
            for (int i = 0; i < SavedGame.OwnedRockets.Length; i++)
            {
                if (SavedGame.OwnedRockets[i] == -1)
                {
                    SavedGame.OwnedRockets[i] = selectedBuyRocket;
                    SavedGame.RocketNames[i] = inputRocketName.text;

                    Debug.Log("Bought rocket at array index " + i.ToString());

                    newRocketIndex = i;
                    break;
                }
            }

            inputRocketName.text = "";
            instRocketnameCanvas.GetComponent<Canvas>().enabled = false;

            //TODO Show rocket bought

            garageCameraLook.IsRight = false;

            setRocketBoughtActive(newRocketIndex);
        }
        else
        {
            GameObject instT = Instantiate(prefabTextTooShort);
            Destroy(instT, 4f);
        }
    }

    private bool isRightATM = true;

    public void CameraSwitchedToRight(bool toRight)
    {
        if (toRight)
        {
            rocketNameTurner.ShowRocketName("Price: " + SavedGame.RocketPrices[selectedBuyRocket]);
        }
        else
        {
            rocketNameTurner.ShowRocketName(SavedGame.RocketNames[selectedBoughtRocket]);
        }
        isRightATM = toRight;
    }

    public void LeftClick()
    {
        if (isRightATM)
        {
            if (rocketNubmber <= 0)
                rocketNubmber = rockets.Length - 1;

            else
                rocketNubmber -= 1;

            print(rocketNubmber);
            setRocketActive(rocketNubmber);
        }
        else
        {
            selectedBoughtRocket--;
            if (selectedBoughtRocket < 0)
            {
                for (int i = SavedGame.OwnedRockets.Length - 1; i >= 0; i--)
                {
                    if (SavedGame.OwnedRockets[i] != -1)
                    {
                        selectedBoughtRocket = i;
                        break;
                    }
                }
            }

            setRocketBoughtActive(selectedBoughtRocket);
        }
    }

    public void RightClick()
    {
        if (isRightATM)
        {
            if (rocketNubmber >= rockets.Length - 1)
                rocketNubmber = 0;

            else
                rocketNubmber += 1;

            print(rocketNubmber);
            setRocketActive(rocketNubmber);
        }
        else
        {
            selectedBoughtRocket++;
            if (SavedGame.OwnedRockets[selectedBoughtRocket] == -1)
            {
                selectedBoughtRocket = 0;
            }

            setRocketBoughtActive(selectedBoughtRocket);
        }
    }
}
