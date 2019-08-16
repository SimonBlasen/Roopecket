using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRocketSpawner : MonoBehaviour
{
    public GameObject[] rockets;

    // Start is called before the first frame update
    void Start()
    {
        
        CameraMultiController cmc = GameObject.FindObjectOfType<CameraMultiController>();

        for (int i = 0; i < rockets.Length; i++)
        {
            if (i == Statics.selectedRocket)
            {
                rockets[i].SetActive(true);
                cmc.rockets = new Transform[] { rockets[i].transform };
                cmc.rocketRigidbody = rockets[i].GetComponent<Rigidbody>();

                if (GameObject.FindObjectOfType<CheckTutorial11>() != null)
                {
                    GameObject.FindObjectOfType<CheckTutorial11>().rocketController = rockets[i].GetComponent<RocketController>();
                }
            }
            else
            {
                rockets[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
