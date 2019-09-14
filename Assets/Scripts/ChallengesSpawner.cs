using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengesSpawner : MonoBehaviour
{
    public GameObject challengesPointerPrefab;
    public PlanetLevel[] levelPositions;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SavedGame.GetChallenges(SavedGame.LastPlayedRocket).Length; i++)
        {
            int chal = SavedGame.GetChallenges(SavedGame.LastPlayedRocket)[i];
            if (chal != -1)
            {
                GameObject instLine = Instantiate(challengesPointerPrefab);
                instLine.transform.position = levelPositions[i].LandingPlatformPosition;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
