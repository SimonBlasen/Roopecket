using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour
{
    public float lerpLookat = 0.1f;
    public float lerpSpeed = 0.1f;
    public Vector3[] positions;
    public Vector3[] lookAts;
    public Transform lookatTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookatTrans.position = Vector3.Lerp(lookatTrans.position, lookAts[Index], lerpLookat);
        transform.position = Vector3.Lerp(transform.position, positions[Index], lerpSpeed);
        transform.LookAt(lookatTrans);
    }

    public int Index
    {
        get;set;
    }
}
