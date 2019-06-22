using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLevel : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boxColliderTriggerMouse;

    // Start is called before the first frame update
    void Start()
    {
        boxColliderTriggerMouse.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
