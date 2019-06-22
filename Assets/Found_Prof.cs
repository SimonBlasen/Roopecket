using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found_Prof : MonoBehaviour
{
    private bool foundProf = false;
    public GameObject profParticles;
    public int profNumber;


    private Camera cam;

    private void Start()
    {
        cam = GameObject.FindObjectOfType<CameraMultiController>().GetComponentInChildren<Camera>();
        profParticles.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (foundProf == false && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetInstanceID() == transform.GetInstanceID())
            {
                foundProf = true;
                profParticles.SetActive(true);
            }
        }

    }
}
