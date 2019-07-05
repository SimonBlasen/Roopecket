using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageButtonPositioning : MonoBehaviour
{
    [SerializeField]
    private GameObject panelWorldTarget;
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    private Vector2 offset;

    private RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        Transform par = transform;
        while (par.parent != null)
        {
            par = par.parent;
        }
        canvasRect = par.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(panelWorldTarget.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        panel.anchoredPosition = WorldObject_ScreenPosition + offset;
    }
}
