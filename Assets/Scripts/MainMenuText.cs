using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuText : MonoBehaviour {

    private RectTransform menuText;
    private Vector3 spawn;
    private Vector3 selectedGarage, selectedContinue, selectedOptions;
    private bool MouseHover = false;
    private bool Entered;
    private bool MousePress = false;
    public string pointName;

    // Use this for initialization
    void Start () {
        spawn = transform.position;
        selectedContinue = new Vector3(0f,0f,-4.15f);
        selectedGarage = new Vector3(0f, 0.1f, -4.15f);
        selectedOptions = new Vector3(0f, -0.1f, -4.15f);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {


        
		
        if (MouseHover)
        {
            if (pointName == "Continue")
            {
                transform.position = selectedContinue;
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 0.5f, 1f);

                if(MousePress)
                {

                    SceneManager.LoadScene("Platform First Level");

                }

            }

            if (pointName == "Garage")
            {
                transform.position = selectedGarage;
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 0.5f, 1f);
            }

            if (pointName == "Options")
            {
                transform.position = selectedOptions;
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 0.5f, 1f);
            }

        }
        else
        {
            transform.position = spawn;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 1f, 1f, 1f);
        }
        

	}

    private void OnMouseEnter()
    {
        MouseHover = true;
        print("OKIOKI");
     //menuText.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);


    }

    private void OnMouseDown()
    {
        MousePress = true;
    }

    private void OnMouseExit()
    {
        MouseHover = false;
    }

}
