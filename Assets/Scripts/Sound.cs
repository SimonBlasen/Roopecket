using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public float pitch;
    public string name;

    public class Sounds
    {

        [HideInInspector]
        public AudioSource source;

    }

}
