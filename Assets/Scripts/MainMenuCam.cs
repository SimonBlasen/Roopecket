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

    public MenuLevelInfo menuLevelInfo;

    private bool zoomedToPlanet = false;
    private Transform planetTarget = null;
    private Transform lookatPlanet = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!zoomedToPlanet)
        {
            lookatTrans.position = Vector3.Lerp(lookatTrans.position, lookAts[Index], lerpLookat);
            transform.position = Vector3.Lerp(transform.position, positions[Index], lerpSpeed);
            transform.LookAt(lookatTrans);
        }
        else
        {
            lookatTrans.position = Vector3.Lerp(lookatTrans.position, lookatPlanet.position, lerpLookat);
            transform.position = Vector3.Lerp(transform.position, planetTarget.position, lerpSpeed);
            transform.LookAt(lookatTrans);
        }
    }

    public int Index
    {
        get;set;
    }

    public void ZoomToPlanet(Transform camPos, Transform planetTarget)
    {
        zoomedToPlanet = true;
        this.planetTarget = camPos;
        lookatPlanet = planetTarget;
    }

    public void UnzoomPlanet()
    {
        zoomedToPlanet = false;
    }
}
