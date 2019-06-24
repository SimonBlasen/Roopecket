﻿using System.Collections;
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

    private void OnApplicationQuit()
    {
        SavedGame.SaveSavegame();
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
