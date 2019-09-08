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
    [SerializeField]
    private Transform optionalPlanetTarget = null;

    private Vector3 scaleTarget = new Vector3(1f, 1f, 1f);
    private Vector3 lowScale;
    private Vector3 highScale;

    private MainMenuCam mainMenuCam;

    // Start is called before the first frame update
    void Start()
    {
        lowScale = transform.localScale;
        scaleTarget = lowScale;
        highScale = lowScale * 1.2f;
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
            if (!disabled)
            {
                hovered = value;

                if (hovered)
                {
                    scaleTarget = highScale;
                }
                else
                {
                    scaleTarget = lowScale;
                }
            }
        }
    }

    bool disabled = false;

    public void Disable()
    {
        disabled = true;
    }

    public void Clicked()
    {
        if (!disabled)
        {
            if (optionalPlanetTarget != null)
            {
                mainMenuCam.ZoomToPlanet(cameraPos, optionalPlanetTarget);
                FindObjectOfType<AudioManager>().Play("Whoosh");
            }
            else
            {
                mainMenuCam.ZoomToPlanet(cameraPos, transform);
                FindObjectOfType<AudioManager>().Play("Whoosh");
            }
            for (int i = 0; i < planetLevels.Length; i++)
            {
                planetLevels[i].IsZoomed = true;
            }
        }
    }

    public void Unzoomed()
    {
        if (!disabled)
        {
            for (int i = 0; i < planetLevels.Length; i++)
            {
                planetLevels[i].IsZoomed = false;
            }
        }
    }
}
