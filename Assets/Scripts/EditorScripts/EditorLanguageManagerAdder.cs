using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EditorLanguageManagerAdder : MonoBehaviour
{
    public bool AddLanguageManagerToObjects = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AddLanguageManagerToObjects)
        {
            AddLanguageManagerToObjects = false;

            TextMeshPro[] tmps = GameObject.FindObjectsOfType<TextMeshPro>();
            TextMeshProUGUI[] tmpUGUIs = GameObject.FindObjectsOfType<TextMeshProUGUI>();
            Text[] textsOldGUI = GameObject.FindObjectsOfType<Text>();

            for (int i = 0; i < tmps.Length; i++)
            {
                if (tmps[i].transform.GetComponent<LanguageManager>() == null)
                {
                    tmps[i].gameObject.AddComponent<LanguageManager>();
                }
            }
            for (int i = 0; i < tmpUGUIs.Length; i++)
            {
                if (tmpUGUIs[i].transform.GetComponent<LanguageManager>() == null)
                {
                    tmpUGUIs[i].gameObject.AddComponent<LanguageManager>();
                }
            }
            for (int i = 0; i < textsOldGUI.Length; i++)
            {
                if (textsOldGUI[i].transform.GetComponent<LanguageManager>() == null)
                {
                    textsOldGUI[i].gameObject.AddComponent<LanguageManager>();
                }
            }

        }
    }
}
