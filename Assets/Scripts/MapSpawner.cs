using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] maps;

    private GameObject instMap = null;


    private bool inited = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        if (!inited)
        {
            inited = true;

            if (instMap == null)
            {
                Destroy(instMap);
            }

            instMap = Instantiate(maps[Statics.selectedMap]);

            MultiplayerMap multiMap = instMap.GetComponent<MultiplayerMap>();

            GameObject startPlatform = GameObject.Find("Start Platform");
            GameObject startPlatformP2 = null;
            
            startPlatformP2 = GameObject.Find("Start Platform P2");

            startPlatform.transform.position = multiMap.StartPlatformPlayer1;
            startPlatformP2.transform.position = multiMap.StartPlatformPlayer2;
            startPlatform.transform.rotation = Quaternion.Euler(multiMap.StartPlatformPlayer1Rotation);
            startPlatformP2.transform.rotation = Quaternion.Euler(multiMap.StartPlatformPlayer2Rotation);



        }
    }

    public void ForceInit()
    {
        inited = false;
        Init();
    }
}
