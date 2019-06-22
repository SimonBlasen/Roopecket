using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRocketSelector : MonoBehaviour
{

    public bool mouseHover = false;
    public bool mousedown = false;
    private bool locked = false;

    public float left = 0;
    public float right = 0;
    public int rocketNubmber = 1;

    public Transform[] rockets;
    public Transform currentRocket;

    public GameObject cage;

    Vector3 spawn;
    Vector3 selected;

    public Vector3 rocketSpawn;

    private bool mouseIsDown = false;

    private void Start()
    {
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
        




    }

    private void setRocketActive(int number)
    {
        if (number >= 0 && number < rockets.Length)
        {
            Statics.selectedRocket = number;

            currentRocket.gameObject.SetActive(false);

            currentRocket = rockets[number];

            currentRocket.gameObject.SetActive(true);
            currentRocket.transform.position = rocketSpawn;
            currentRocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentRocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            
            cage.SetActive(SavedGame.OwnedRockets[number] != -1);
            
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
