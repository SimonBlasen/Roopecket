using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEndscreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;
    [SerializeField]
    private TextMeshProUGUI textMeshTitle;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string SceneToLoad
    {
        get;set;
    }

    public string ShowText
    {
        get
        {
            return textMesh.text;
        }
        set
        {
            textMesh.text = value;
        }
    }

    public string TitleText
    {
        get
        {
            return textMeshTitle.text;
        }
        set
        {
            textMeshTitle.text = value;
        }
    }

    public void ButtonNextClick()
    {
        if (GetComponent<Canvas>().enabled)
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }

    public void ButtonTryagainClick()
    {
        if (GetComponent<Canvas>().enabled)
        {
            Debug.Log("Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ButtonBackToMenuClick()
    {
        if (GetComponent<Canvas>().enabled)
        {
            SceneManager.LoadScene("Main_Menu_3");
        }
    }

    public void ButtonGoFreestyleClick()
    {
        if (GetComponent<Canvas>().enabled)
        {
            SceneManager.LoadScene("Tutorial Freestyle");
        }
    }
}
