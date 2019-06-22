using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicSceneLoader : MonoBehaviour
{
    public GameObject[] scenes;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if (i == Statics.comicSceneToLoad)
            {
                scenes[i].SetActive(true);
            }
            else
            {
                scenes[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
