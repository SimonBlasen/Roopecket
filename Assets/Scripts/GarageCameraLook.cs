using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCameraLook : MonoBehaviour
{
    [SerializeField]
    private ArrowRocketSelector arrowRocketSelector;

    [SerializeField]
    [Range(0f, 1f)]
    private float changeToLeftPercent = 0.2f;
    [SerializeField]
    [Range(0f, 1f)]
    private float changeToRightPercent = 0.2f;
    [SerializeField]
    private float childMoveFactor = 1f;
    [SerializeField]
    private float lerpSpeedChild = 0.1f;
    [SerializeField]
    private float lerpSpeedMove = 0.1f;

    private bool isRight = true;

    [SerializeField]
    private Transform posRight;
    [SerializeField]
    private Transform posLeft;
    [SerializeField]
    private Transform lookatParent;
    [SerializeField]
    private Transform lookatChild;
    [SerializeField]
    private Transform lookatPosRight;
    [SerializeField]
    private Transform lookatPosLeft;

    // Start is called before the first frame update
    void Start()
    {
        if (Statics.startGarageLeft)
        {
            Statics.startGarageLeft = false;

            isRight = false;
            arrowRocketSelector.CameraSwitchedToRight(isRight);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePos = Input.mousePosition;

        mousePos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);
        if (mousePos.x < 0f)
        {
            mousePos.x = 0f;
        }
        else if (mousePos.x > 1f)
        {
            mousePos.x = 1f;
        }
        if (mousePos.y < 0f)
        {
            mousePos.y = 0f;
        }
        else if (mousePos.y > 1f)
        {
            mousePos.y = 1f;
        }

        lookatChild.localPosition = Vector3.Lerp(lookatChild.localPosition, (new Vector3(mousePos.x - 0.5f, mousePos.y - 0.5f, 0f)) * childMoveFactor, lerpSpeedChild);


        transform.LookAt(lookatChild);

        if (isRight)
        {
            transform.position = Vector3.Lerp(transform.position, posRight.position, lerpSpeedMove);
            lookatParent.position = Vector3.Lerp(lookatParent.position, lookatPosRight.position, lerpSpeedMove);
            lookatParent.rotation = Quaternion.Lerp(lookatParent.rotation, lookatPosRight.rotation, 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, posLeft.position, lerpSpeedMove);
            lookatParent.position = Vector3.Lerp(lookatParent.position, lookatPosLeft.position, lerpSpeedMove);
            lookatParent.rotation = Quaternion.Lerp(lookatParent.rotation, lookatPosLeft.rotation, 0.1f);
        }



        // Change
        if (mousePos.y < 0.8f)
        {
            if (isRight && mousePos.x < changeToLeftPercent)
            {
                isRight = false;
                arrowRocketSelector.CameraSwitchedToRight(isRight);
            }
            else if (isRight == false && 1f - mousePos.x < changeToRightPercent)
            {
                isRight = true;
                arrowRocketSelector.CameraSwitchedToRight(isRight);
            }
        }
    }

    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value;
            arrowRocketSelector.CameraSwitchedToRight(isRight);
        }
    }
}
