using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerMap : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPlatformPos;
    [SerializeField]
    private Vector3 startPlatform2Pos;
    [SerializeField]
    private Vector3 startPlatformRot;
    [SerializeField]
    private Vector3 startPlatform2Rot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 StartPlatformPlayer1
    {
        get
        {
            return startPlatformPos;
        }
    }

    public Vector3 StartPlatformPlayer2
    {
        get
        {
            return startPlatform2Pos;
        }
    }

    public Vector3 StartPlatformPlayer1Rotation
    {
        get
        {
            return startPlatformRot;
        }
    }

    public Vector3 StartPlatformPlayer2Rotation
    {
        get
        {
            return startPlatform2Rot;
        }
    }
}
