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
    public GameObject prefabRocketSellCanvas;
    private GameObject instRocketnameCanvas;
    private GameObject instRocketSellCanvas;
    private TMP_InputField inputRocketName;
    public GarageArrows[] arrows;
    public GarageCameraLook garageCameraLook;
    public Button buttonBuyRocket;
    public GameObject ownNoRocket;

    public MoneyMachine moneyMachine;
    public MoneyMachine moneyMachineSell;

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


        instRocketSellCanvas = Instantiate(prefabRocketSellCanvas);
        instRocketSellCanvas.GetComponent<Canvas>().enabled = false;
        Button[] buttonsSell = instRocketSellCanvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttonsSell.Length; i++)
        {
            if (buttonsSell[i].gameObject.name == "Button Yes")
            {
                buttonsSell[i].onClick.AddListener(RocketSellYes);
            }
            else if (buttonsSell[i].gameObject.name == "Button No")
            {
                buttonsSell[i].onClick.AddListener(RocketSellNo);
            }
        }


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
        setRocketBoughtActive(SavedGame.LastPlayedRocket);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputRocketName.isFocused)
        {
            ConfirmRocketNameClick();

        }
    }

    private int selectedBoughtRocket = 0;
    private int selectedBoughtRocketWorth = 0;

    private void setRocketBoughtActive(int index)
    {
        if (index >= 0 && index < SavedGame.OwnedRockets.Length)
        {
            selectedBoughtRocket = index;
            int rocketType = SavedGame.OwnedRockets[selectedBoughtRocket];

            if (rocketType == -1)
            {
                ownNoRocket.SetActive(true);
                for (int i = 0; i < rocketsBought.Length; i++)
                {
                    rocketsBought[i].gameObject.SetActive(false);
                }
            }
            else
            {
                ownNoRocket.SetActive(false);

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

                float worthSum = 0f;

                for (int i = 0; i < 4; i++)
                {
                    if (LevelNumber.GetFirstLevelOfStage(i + 1) <= SavedGame.NextLevel[index])
                    {
                        worthSum += SavedGame.GetGlobalWorthStage(index, i);
                        Debug.Log("Worth calculated for stage: " + i.ToString());
                    }
                }

                selectedBoughtRocketWorth = (int)worthSum;
                moneyMachineSell.Number = selectedBoughtRocketWorth;

                selectedBoughtRocketWorthPlusPrice = selectedBoughtRocketWorth + SavedGame.RocketPrices[rocketType];

                SavedGame.LastPlayedRocket = index;
            }

            
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

    private int selectedBoughtRocketWorthPlusPrice = 0;

    public void SellRocketClick()
    {
        instRocketSellCanvas.GetComponent<Canvas>().enabled = true;
        TextMeshProUGUI[] tmps = instRocketSellCanvas.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < tmps.Length; i++)
        {
            if (tmps[i].gameObject.name == "Text Main")
            {
                string priceStr = selectedBoughtRocketWorthPlusPrice.ToString();
                int pointsInst = 0;
                for (int j = 3; j < priceStr.Length; j += 3)
                {
                    priceStr = priceStr.Insert(priceStr.Length - j + pointsInst, ".");
                    j++;
                    
                }


                tmps[i].text = "Are you sure to sell your rocket\nfor " + priceStr + "$?";

                break;
            }
        }
    }

    public void RocketSellYes()
    {
        SavedGame.Money += selectedBoughtRocketWorthPlusPrice;
        moneyMachine.Number = SavedGame.Money;

        SavedGame.OwnedRockets[selectedBoughtRocket] = -1;
        SavedGame.NextLevel[selectedBoughtRocket] = 0;
        SavedGame.RocketNames[selectedBoughtRocket] = "";

        for (int i = 0; i < 20; i++)
        {
            SavedGame.CurrentDamageStage[selectedBoughtRocket, i] = 0f;
            SavedGame.CurrentTimeStage[selectedBoughtRocket, i] = 0f;
            SavedGame.CurrentUsedFuel[selectedBoughtRocket, i] = 0f;
        }

        RightClick();

        garageCameraLook.IsRight = true;

        if (SavedGame.Money >= SavedGame.RocketPrices[selectedBuyRocket])
        {
            buttonBuyRocket.interactable = true;
        }
        else
        {
            buttonBuyRocket.interactable = false;
        }

        instRocketSellCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void RocketSellNo()
    {
        instRocketSellCanvas.GetComponent<Canvas>().enabled = false;
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
            while (selectedBoughtRocket >= 0 && SavedGame.OwnedRockets[selectedBoughtRocket] == -1)
            {
                selectedBoughtRocket--;
            }
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
            int cnt = 0;
            while (SavedGame.OwnedRockets[selectedBoughtRocket] == -1 && cnt <= 256)
            {
                cnt++;
                selectedBoughtRocket++;
                selectedBoughtRocket = selectedBoughtRocket % SavedGame.OwnedRockets.Length;
            }
            if (SavedGame.OwnedRockets[selectedBoughtRocket] == -1)
            {
                selectedBoughtRocket = 0;
            }

            setRocketBoughtActive(selectedBoughtRocket);
        }
    }
}
