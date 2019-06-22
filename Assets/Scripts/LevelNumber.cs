using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNumber : MonoBehaviour
{
    public int levelNumber = -1;



    // Start is called before the first frame update
    void Start()
    {
        if (levelNumber == -1)
        {
            Debug.LogError("Need to set the level number for the level");
        }
        else
        {
            Statics.currentLevel = levelNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
