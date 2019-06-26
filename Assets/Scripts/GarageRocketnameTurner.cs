using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarageRocketnameTurner : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text1;
    [SerializeField]
    private TextMeshPro text2;
    [SerializeField]
    private Transform flipTrans;
    [SerializeField]
    private Vector3 frontRot;
    [SerializeField]
    private Vector3 backRot;
    [SerializeField]
    private float lerpSpeed = 0.1f;

    private bool textFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        text1.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        flipTrans.localRotation = Quaternion.Lerp(flipTrans.localRotation, textFlipped ? Quaternion.Euler(backRot) : Quaternion.Euler(frontRot), lerpSpeed);
    }

    public void ShowRocketName(string name)
    {
        if (textFlipped)
        {
            text1.text = name;
        }
        else
        {
            text2.text = name;
        }

        textFlipped = !textFlipped;
    }
}
