using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLevel : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boxColliderTriggerMouse;
    [SerializeField]
    private Transform scaleChild;
    [SerializeField]
    private float lerpScaleSpeed = 0.07f;


    private Vector3 scaleTarget = new Vector3(1.0f, 1.0f, 1.0f);
    private MainMenuCam mainMenuCam;

    // Start is called before the first frame update
    void Start()
    {
        boxColliderTriggerMouse.enabled = false;
        mainMenuCam = GameObject.FindObjectOfType<MainMenuCam>();
    }

    // Update is called once per frame
    void Update()
    {
        scaleChild.localScale = Vector3.Lerp(scaleChild.localScale, scaleTarget, lerpScaleSpeed);
    }

    public void Clicked()
    {
        Debug.Log("Clicked Level");

        mainMenuCam.menuLevelInfo.panelWorldTarget = scaleChild.gameObject;
    }


    public void Unclicked()
    {
        mainMenuCam.menuLevelInfo.panelWorldTarget = null;
    }


    private bool isZoomed = false;
    public bool IsZoomed
    {
        get
        {
            return isZoomed;
        }
        set
        {
            isZoomed = value;

            boxColliderTriggerMouse.enabled = isZoomed;
        }
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

            Debug.Log("Level hovered " + hovered.ToString());

            if (hovered)
            {
                scaleTarget = new Vector3(1.3f, 1.0f, 1.3f);
            }
            else
            {
                scaleTarget = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
