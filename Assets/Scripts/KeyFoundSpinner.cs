using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFoundSpinner : MonoBehaviour
{
    public float ySpeedMax = 5000f;
    public float angleAcc = 100f;
    public float yAcc = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float ySpeed = 0f;
    private float ySpeedUp = 0f;
    private bool spinning = false;

    // Update is called once per frame
    void Update()
    {
        if (spinning)
        {
            ySpeed += Time.deltaTime * angleAcc;
            transform.Rotate(Vector3.up, ySpeed * Time.deltaTime);

            if (ySpeed >= ySpeedMax && transform.position.y < 2000f)
            {
                ySpeed = ySpeedMax;
                ySpeedUp += Time.deltaTime * yAcc;
                transform.position += new Vector3(0f, ySpeedUp * Time.deltaTime, 0f);
            }
        }
    }

    public void StartSpinning()
    {
        spinning = true;
    }
}
