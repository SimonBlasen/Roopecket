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
        if (GameObject.FindObjectOfType<CameraMultiController>() != null)
        {
            cam = GameObject.FindObjectOfType<CameraMultiController>().GetComponentInChildren<Camera>();
        }
        else if (cam == null)
        {
            cam = Camera.main;
        }
        profParticles.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (foundProf == false && Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetInstanceID() == transform.GetInstanceID())
                {
                    foundProf = true;
                    profParticles.SetActive(true);
                    SavedGame.DrEberhardtFound[Statics.currentLevel] = true;
                }
            }
        }

    }
}
