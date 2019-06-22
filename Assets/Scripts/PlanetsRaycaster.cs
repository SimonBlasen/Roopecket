using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsRaycaster : MonoBehaviour
{
    private MenuPlanet lastHoveredPlanet = null;

    private bool zoomedInOnPlanet = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
