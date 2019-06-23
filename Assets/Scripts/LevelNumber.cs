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
            levelNumber--;
            Statics.currentLevel = levelNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static int GetStage(int levelNumber)
    {
        if (levelNumber >= 0 && levelNumber <= 5)
        {
            return 0;
        }
        if (levelNumber >= 6 && levelNumber <= 10)
        {
            return 1;
        }
        if (levelNumber >= 11 && levelNumber <= 15)
        {
            return 2;
        }
        if (levelNumber >= 16)
        {
            return 3;
        }
        return -1;
    }

    public static int GetFirstLevelOfStage(int stage)
    {
        for (int i = 0; i <= 20; i++)
        {
            if (GetStage(i) >= stage)
            {
                return i;
            }
        }

        return -1;
    }
}
