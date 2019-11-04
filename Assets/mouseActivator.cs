using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseActivator : MonoBehaviour
{
    private float mouseVisibleTime = 2f;
    private Vector3 MousePosition;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


    private Coroutine co_HideCursor;

    void Update()
    {
        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0))
        {
            if (co_HideCursor == null)
            {
                co_HideCursor = StartCoroutine(HideCursor());
            }
        }
        else
        {
            if (co_HideCursor != null)
            {
                StopCoroutine(co_HideCursor);
                co_HideCursor = null;
                Cursor.visible = true;
            }
        }
    }

    private IEnumerator HideCursor()
    {
        yield return new WaitForSeconds(2);
        Cursor.visible = false;
    }

    /* void Update()
     {



         if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
         {

             Cursor.visible = true;
             //  Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
             mouseVisibleTime = 2f;

             if (mouseVisibleTime > 0)
             {

                 mouseVisibleTime -= Time.deltaTime;

             }


             else
             {

                 Cursor.visible = false;


             }
             Debug.Log(mouseVisibleTime);


         }

     }


     void MouseisVisibleForTime()
     {






     }

     */
}

