using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelInfo : MonoBehaviour
{
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    public GameObject panelWorldTarget = null;

    RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panelWorldTarget != null)
        {
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(panelWorldTarget.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            panel.anchoredPosition = WorldObject_ScreenPosition;
        }
    }
}
