using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsRaycaster : MonoBehaviour
{
    private MenuPlanet lastHoveredPlanet = null;
    private PlanetLevel lastHoveredLevel = null;

    private MainMenuCam mainMenuCam;
    private bool zoomedInOnPlanet = false;

    private bool selectedLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCam = GameObject.FindObjectOfType<MainMenuCam>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (zoomedInOnPlanet == false)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<MenuPlanet>() != null)
                {
                    if (lastHoveredPlanet != null)
                    {
                        lastHoveredPlanet.Hovered = false;
                        lastHoveredPlanet = null;
                    }
                    lastHoveredPlanet = hit.transform.GetComponent<MenuPlanet>();
                    lastHoveredPlanet.Hovered = true;
                }
                else
                {
                    if (lastHoveredPlanet != null)
                    {
                        lastHoveredPlanet.Hovered = false;
                        lastHoveredPlanet = null;
                    }
                }
            }
            else
            {
                if (lastHoveredPlanet != null)
                {
                    lastHoveredPlanet.Hovered = false;
                    lastHoveredPlanet = null;
                }
            }



            if (Input.GetMouseButtonDown(0) && lastHoveredPlanet != null)
            {
                lastHoveredPlanet.Clicked();
                lastHoveredPlanet.Hovered = false;
                lastHoveredPlanet = null;
                zoomedInOnPlanet = true;
            }
        }
        else
        {
            if (selectedLevel == false)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.GetComponentInParent<PlanetLevel>() != null)
                    {
                        if (lastHoveredLevel != null)
                        {
                            lastHoveredLevel.Hovered = false;
                            lastHoveredLevel = null;
                        }

                        lastHoveredLevel = hit.transform.GetComponentInParent<PlanetLevel>();
                        lastHoveredLevel.Hovered = true;
                    }
                    else
                    {
                        if (lastHoveredLevel != null)
                        {
                            lastHoveredLevel.Hovered = false;
                            lastHoveredLevel = null;
                        }
                    }
                }
                else
                {
                    if (lastHoveredLevel != null)
                    {
                        lastHoveredLevel.Hovered = false;
                        lastHoveredLevel = null;
                    }
                }








                if (Input.GetMouseButtonDown(0) && lastHoveredLevel != null)
                {
                    lastHoveredLevel.Clicked();
                    selectedLevel = true;
                    //lastHoveredLevel.Hovered = false;
                    //lastHoveredLevel = null;
                }
                else if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) == false)
                {
                    ZoomedInOnPlanet = false;
                    mainMenuCam.UnzoomPlanet();
                }
            }
            else
            {
                // Wenn level angeklick ist

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit) == false || hit.transform.GetComponentInParent<PlanetLevel>() == null)
                    {
                        selectedLevel = false;
                        lastHoveredLevel.Unclicked();
                    }
                }


            }


        }

    }


    public bool ZoomedInOnPlanet
    {
        get
        {
            return zoomedInOnPlanet;
        }
        set
        {
            zoomedInOnPlanet = value;
        }
    }
}
