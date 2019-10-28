using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylRotator : MonoBehaviour
{
    [SerializeField]
    private Transform earthTrans;
    [SerializeField]
    private float earthRotationSpeed;

    private void Update()
    {
        for (int i = 0; i < 360; i++)
        {

            //earthTrans.rotation.y += earthRotationSpeed * Time.deltaTime;
            earthTrans.rotation = Quaternion.Euler(earthTrans.rotation.eulerAngles.x, earthTrans.rotation.eulerAngles.y + earthRotationSpeed * Time.deltaTime, earthTrans.rotation.eulerAngles.z);
        }
    }
}
