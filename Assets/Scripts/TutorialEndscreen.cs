using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEndscreen : MonoBehaviour
{
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
}
