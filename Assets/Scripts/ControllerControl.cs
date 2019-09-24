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
            for (int i = 0; i < amountThrusters; i++)
            {
                rocketController.SetThrust(i, sampleThruster(i, amountThrusters, ls, rs));
                //rocketController.SetThrust(i, ls * ((amountThrusters - i) / ((float)amountThrusters)) + rs * ((i) / ((float)amountThrusters)));
                //vals[i] = ls * ((amountThrusters - i) / ((float)amountThrusters)) + rs * ((i) / ((float)amountThrusters));
                vals[i] = sampleThruster(i, amountThrusters, ls, rs);
                str += vals[i] + ", ";
            }
            Debug.Log(str);
        }
    }

    private float sampleThruster(int index, int thrusters, float left, float right)
    {
        float pos = index / (thrusters - 1f);

        float fromLeft = Mathf.Pow(Mathf.Cos(pos * (Mathf.PI / 2f)), 2f) * left;
        float fromRight = Mathf.Pow(Mathf.Sin(pos * (Mathf.PI / 2f)), 2f) * right;

        return fromLeft + fromRight;

    }
}
