using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicContinuous : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] MusicList;

    public int[] levels;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        Debug.Log("Dont destroy music");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int oldLevel = -90;

    private void setLevel(int level)
    {
        Debug.Log("Level: " + level.ToString() + "\nOld: " + oldLevel);
        level++;
        if (levels[level] != oldLevel)
        {
            oldLevel = levels[level];
            if (level == -1)
            {
                audioSource.clip = MusicList[0];
            }
            else
            {
                audioSource.clip = MusicList[levels[level]];
            }

            Debug.Log("Level: " + level.ToString() + "\nClip: " + audioSource.clip.name);

            audioSource.Play();
        }
    }

    private string oldScene = "";

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != oldScene)
        {
            oldScene = SceneManager.GetActiveScene().name;

            if (oldScene == "Platform First Level")
                setLevel(0);
            else if (oldScene == "Platform sec Level")
                setLevel(1);
            else if (oldScene == "Platform third Level")
                setLevel(2);
            else if (oldScene == "Platform fourth Level")
                setLevel(3);
            else if (oldScene == "Platform fivth Level")
                setLevel(4);
            else if (oldScene == "Platform sixth Level")
                setLevel(5);
            else if (oldScene == "Platform seventh Level")
                setLevel(6);
            else if (oldScene == "Platform eighth Level")
                setLevel(7);
            else if (oldScene == "Platform ninth Level")
                setLevel(8);
            else if (oldScene == "Platform Level 10")
                setLevel(9);
            else if (oldScene == "Platform Level 11")
                setLevel(10);
            else if (oldScene == "Platform Level 12 CR")
                setLevel(11);
            else if (oldScene == "Platform Level 13 CR")
                setLevel(12);
            else if (oldScene == "Platform Level 14 CR space")
                setLevel(13);
            else if (oldScene == "Platform Level 15")
                setLevel(14);
            else if (oldScene == "Platform Level 16")
                setLevel(15);
            else if (oldScene == "Platform Level 17")
                setLevel(16);
            else if (oldScene == "Platform Level 18")
                setLevel(17);
            else if (oldScene == "Platform Level 19")
                setLevel(18);
            else if (oldScene == "Platform Level 20")
                setLevel(19);
            else
                setLevel(-1);

        }
    }

}
