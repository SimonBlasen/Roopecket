using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicContinuous : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip[] MusicList;


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

    // Update is called once per frame
    void Update()
    {
        
    }
}
