using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerControl : MonoBehaviour
{
    RocketSpawner rocketSpawner;
    public RocketController rocketController;
    public RocketController rocketControllerP1;

    public int gamepadNumber = 0;

    private bool wasDifferentFromZero = false;

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
        float ls2 = Input.GetAxis("ControllerLS2");
        float rs2 = Input.GetAxis("ControllerRS2");

        if (ls != 0f || rs != 0f)
        {
            wasDifferentFromZero = true;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (rocketController == null)
        {
            if (rocketSpawner != null)
            {
                if (rocketSpawner.SpawnedRocket != null)
                {
                    if (rocketSpawner.Spawn2Rockets)
                    {
                        rocketController = rocketSpawner.SpawnedRocket2.GetComponent<RocketController>();
                        rocketControllerP1 = rocketSpawner.SpawnedRocket.GetComponent<RocketController>();
                    }
                    else
                    {
                        rocketController = rocketSpawner.SpawnedRocket.GetComponent<RocketController>();
                        rocketControllerP1 = null;
                    }
                }
            }
            else
            {
                rocketController = GameObject.FindObjectOfType<RocketController>();
            }
        }
        else if (wasDifferentFromZero)
        {
            if (rocketController.UseController == false)
            {
                rocketController.UseController = true;
            }
            if (rocketControllerP1 != null && rocketControllerP1.UseController == false)
            {
                rocketControllerP1.UseController = true;
            }

            int amountThrusters = rocketController.Thrusts.Length;

            float[] vals = new float[amountThrusters];
            string str = "";
            for (int i = 0; i < amountThrusters; i++)
            {
                if (Statics.isSplitscreen == false || Statics.deviceP2 == 1)
                {
                    rocketController.SetThrust(i, sampleThruster(i, amountThrusters, ls, rs));
                }
                else if (Statics.deviceP2 == 2)
                {
                    rocketController.SetThrust(i, sampleThruster(i, amountThrusters, ls2, rs2));
                }

                if (rocketControllerP1 != null)
                {
                    if (Statics.deviceP1 == 1)
                    {
                        rocketControllerP1.SetThrust(i, sampleThruster(i, amountThrusters, ls, rs));
                    }
                    else if (Statics.deviceP1 == 2)
                    {
                        rocketControllerP1.SetThrust(i, sampleThruster(i, amountThrusters, ls2, rs2));
                    }
                }

                //rocketController.SetThrust(i, ls * ((amountThrusters - i) / ((float)amountThrusters)) + rs * ((i) / ((float)amountThrusters)));
                //vals[i] = ls * ((amountThrusters - i) / ((float)amountThrusters)) + rs * ((i) / ((float)amountThrusters));
                vals[i] = sampleThruster(i, amountThrusters, ls, rs);
                str += vals[i] + ", ";
            }
            //Debug.Log(str);
        }
    }

    private float sampleThruster(int index, int thrusters, float left, float right)
    {
        if (thrusters == 1)
        {
            return Mathf.Max(left, right);
        }
        else
        {
            float pos = index / (thrusters - 1f);

            float fromLeft = Mathf.Pow(Mathf.Cos(pos * (Mathf.PI / 2f)), 2f) * left;
            float fromRight = Mathf.Pow(Mathf.Sin(pos * (Mathf.PI / 2f)), 2f) * right;

            return fromLeft + fromRight;
        }

    }
}
