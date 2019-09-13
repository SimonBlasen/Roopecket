using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFindDetector : MonoBehaviour
{
    public AudioClip clipFound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool found = false;

    private void OnTriggerEnter(Collider other)
    {
        if (found == false && other.tag == "Rocket")
        {
            Debug.Log("Keyfound");
            found = true;
            SavedGame.RocketUnlockKeys++;

            if (clipFound != null)
            {
                GetComponent<AudioSource>().clip = clipFound;
                GetComponent<AudioSource>().Play();
            }
            GetComponentInParent<KeyFoundSpinner>().StartSpinning();
        }
    }
}
