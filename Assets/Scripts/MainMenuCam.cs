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
    private CameraMoverMainmenu cmm;

    public MenuLevelInfo menuLevelInfo;

    private bool zoomedToPlanet = false;
    private Transform planetTarget = null;
    private Transform lookatPlanet = null;

    // Start is called before the first frame update
    void Start()
    {
        cmm = GetComponent<CameraMoverMainmenu>();

        cmm.SetLookat(lookAts[Index]);
        cmm.SetPosition(positions[Index]);
    }

    // Update is called once per frame
    void Update()
    {/*
        if (!zoomedToPlanet)
        {
            cmm.SetLookat(lookAts[Index]);
            cmm.SetPosition(positions[Index]);
            //lookatTrans.position = Vector3.Lerp(lookatTrans.position, lookAts[Index], lerpLookat);
            //transform.position = Vector3.Lerp(transform.position, positions[Index], lerpSpeed);
            //transform.LookAt(lookatTrans);
        }
        else
        {
            cmm.SetLookat(lookatPlanet.position);
            cmm.SetPosition(planetTarget.position);
            //lookatTrans.position = Vector3.Lerp(lookatTrans.position, lookatPlanet.position, lerpLookat);
            //transform.position = Vector3.Lerp(transform.position, planetTarget.position, lerpSpeed);
            //transform.LookAt(lookatTrans);
        }*/
    }

    private int index = 0;

    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            cmm.SetLookat(lookAts[Index]);
            cmm.SetPosition(positions[Index]);
        }
    }

    public void ZoomToPlanet(Transform camPos, Transform planetTarget)
    {
        zoomedToPlanet = true;
        this.planetTarget = camPos;
        lookatPlanet = planetTarget;

        cmm.SetLookat(lookatPlanet.position);
        cmm.SetPosition(this.planetTarget.position);
    }

    public void UnzoomPlanet()
    {
        zoomedToPlanet = false;
        cmm.SetLookat(lookAts[Index]);
        cmm.SetPosition(positions[Index]);
    }
}
