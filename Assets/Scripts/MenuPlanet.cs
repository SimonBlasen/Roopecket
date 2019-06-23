using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlanet : MonoBehaviour
{
    [SerializeField]
    private PlanetLevel[] planetLevels;
    [SerializeField]
    private float lerpScaleSpeed = 0.07f;
    [SerializeField]
    private Transform cameraPos;

    private Vector3 scaleTarget = new Vector3(1f, 1f, 1f);

    private MainMenuCam mainMenuCam;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCam = GameObject.FindObjectOfType<MainMenuCam>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, lerpScaleSpeed);
    }


    private bool hovered = false;
    public bool Hovered
    {
        get
        {
            return hovered;
        }
        set
        {
            hovered = value;

            if (hovered)
            {
                scaleTarget = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                scaleTarget = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void Clicked()
    {
        mainMenuCam.ZoomToPlanet(cameraPos, transform);
        for (int i = 0; i < planetLevels.Length; i++)
        {
            planetLevels[i].IsZoomed = true;
        }
    }

    public void Unzoomed()
    {
        for (int i = 0; i < planetLevels.Length; i++)
        {
            planetLevels[i].IsZoomed = false;
        }
    }
}
