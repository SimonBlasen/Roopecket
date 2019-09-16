using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyParticlesDisolve : MonoBehaviour
{

    private float lifetime = 3;
    
    public GameObject particles;
    


    // Start is called before the first frame update
    void Start()
    {
        lifetime = 3;
        
    }

    // Update is called once per frame
    void Update()
    {

        lifetime -= Time.deltaTime;

        if (lifetime <= 0 && particles != null)
        {
            FindObjectOfType<AudioManager>().Play("MenuButton");

        }

    }
}
