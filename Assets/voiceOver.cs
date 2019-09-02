using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiceOver : MonoBehaviour
{


    public AudioClip voiceOver1, voiceOver2, voiceOver3, voiceOver4, voiceOver5;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = voiceOver1;
        
    }
    // Update is called once per frame
    void Update()
    {


   

    }
}
