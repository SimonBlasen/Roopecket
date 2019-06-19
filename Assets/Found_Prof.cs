using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found_Prof : MonoBehaviour
{
    private bool foundProf = false;
    public GameObject profParticles;
    public int profNumber;



    private void Start()
    {
        profParticles.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
     
        if(foundProf)
        {
            profParticles.SetActive(true);
        }

    }

    private void OnMouseDown()
    {

        foundProf = true;

    }
}
