using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();

        }
    }

}
