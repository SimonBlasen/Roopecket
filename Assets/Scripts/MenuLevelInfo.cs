using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelInfo : MonoBehaviour
{
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    public GameObject panelWorldTarget = null;
    [SerializeField]
    public Vector2 offset;

    RectTransform canvasRect;
    Canvas selfCanvas;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        selfCanvas = GetComponent<Canvas>();
        selfCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (panelWorldTarget != null)
        {
            if (selfCanvas.enabled == false)
            {
                selfCanvas.enabled = true;
            }
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(panelWorldTarget.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            panel.anchoredPosition = WorldObject_ScreenPosition + offset;
        }
        else
        {
            if (selfCanvas.enabled)
            {
                selfCanvas.enabled = false;
            }
        }
    }
}
