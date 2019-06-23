using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLevel : MonoBehaviour
{
    [SerializeField]
    private int levelIndex = -1;
    [SerializeField]
    private BoxCollider boxColliderTriggerMouse;
    [SerializeField]
    private Transform scaleChild;
    [SerializeField]
    private float lerpScaleSpeed = 0.07f;
    [SerializeField]
    private Transform currentLevelTransf;
    [SerializeField]
    private Vector3 defScale = new Vector3(1f, 1f, 1f);
    [SerializeField]
    private Vector3 hoverScale = new Vector3(1.3f, 1f, 1.3f);


    private Vector3 scaleTarget = new Vector3(1.0f, 1.0f, 1.0f);
    private MainMenuCam mainMenuCam;

    // Start is called before the first frame update
    void Start()
    {
        scaleTarget = defScale;
        boxColliderTriggerMouse.enabled = false;
        mainMenuCam = GameObject.FindObjectOfType<MainMenuCam>();


        if (levelIndex == SavedGame.NextLevel[Statics.selectedRocket])
        {
            GameObject instLittle = Instantiate(GameObject.FindObjectOfType<LittleRocketsSpawner>().LittleRockets[Statics.selectedRocket], currentLevelTransf.transform);
            instLittle.transform.localPosition = Vector3.zero;
            instLittle.transform.localRotation = Quaternion.identity;
            instLittle.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleChild.localScale = Vector3.Lerp(scaleChild.localScale, scaleTarget, lerpScaleSpeed);
    }

    public void Clicked()
    {
        Debug.Log("Clicked Level");

        mainMenuCam.menuLevelInfo.LevelIndex = levelIndex;
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

            //Debug.Log("Level hovered " + hovered.ToString());

            if (hovered)
            {
                scaleTarget = hoverScale;
            }
            else
            {
                scaleTarget = defScale;
            }
        }
    }
}
