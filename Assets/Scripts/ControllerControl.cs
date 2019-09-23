using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerControl : MonoBehaviour
{
    RocketSpawner rocketSpawner;
    public RocketController rocketController;

    // Start is called before the first frame update
    void Start()
    {
        rocketSpawner = GameObject.FindObjectOfType<RocketSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        float ls = Input.GetAxis("ControllerLS");
        float rs = Input.GetAxis("ControllerRS");

        if (rocketController == null)
        {
            if (rocketSpawner != null)
            {
                if (rocketSpawner.SpawnedRocket != null)
                {
                    rocketController = rocketSpawner.SpawnedRocket.GetComponent<RocketController>();
                }
            }
        }
        else
        {
            if (rocketController.UseController == false)
            {
                rocketController.UseController = true;
            }

            int amountThrusters = rocketController.Thrusts.Length;

            float[] vals = new float[amountThrusters];
            string str = "";
            amountThrusters--;
            for (int i = 0; i <= amountThrusters; i++)
            {
                rocketController.SetThrust(i, ls * ((amountThrusters - i) / ((float)amountThrusters)) + rs * ((i) / ((float)amountThrusters)));
                vals[i] = ls * ((amountThrusters - i) / ((float)amountThrusters)) + rs * ((i) / ((float)amountThrusters));
                str += vals[i] + ", ";
            }
            Debug.Log(str);
        }
    }
}
