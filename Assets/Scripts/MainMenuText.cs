using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuText : MonoBehaviour {

    private RectTransform menuText;
    private Vector3 spawn;
    private Vector3 selectedGarage, selectedContinue, selectedOptions, selectedTutorial, selectedQuit;
    private bool MouseHover = false;
    private bool Entered;
    private bool MousePress = false;
    private bool FirstTouch = false;
    private bool optionsOpened;
    public string pointName;
    public Vector3 hidedButton;
    private float buttonsHoverLerpSpeed = 0.05f;

    public GameObject Options;
    public GameObject Background;

    // Use this for initialization
    void Start () {
        MouseHover = false;
        spawn = transform.position;

        selectedContinue = new Vector3(0f, 0.05f, -4.15f);
        selectedGarage = new Vector3(0f, 0.15f, -4.15f);
        selectedOptions = new Vector3(0f, -0.05f, -4.15f);
        selectedTutorial = new Vector3(0f, -0.15f, -4.15f);
        selectedQuit = new Vector3(0f, -0.8f, -4f);

        hidedButton = new Vector3(100f, 100f, 100f);
        FirstTouch = false;
       
           
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            if(pointName != "Quit")
            renderer.color = new Color(1f, 1f, 0.5f, 0.05f);



    }

    // Update is called once per frame
    void Update()
    {




        if (FirstTouch)

        {



            if (MouseHover)
            {
                if (pointName == "Continue" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedContinue, buttonsHoverLerpSpeed);
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 1f, 0.3f, 1f);

                    if (MousePress)
                    {

                        SceneManager.LoadScene("ComicScene");

                    }

                }



                else if (pointName == "Garage" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedGarage, buttonsHoverLerpSpeed);
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 1f, 0.5f, 1f);

                    if (MousePress)
                    {

                        SceneManager.LoadScene("Garage");

                    }
                }

                else if (pointName == "Quit" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedQuit, buttonsHoverLerpSpeed);
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 0.5f, 0f, 1f);

                    if (MousePress)
                    {

                        Application.Quit();

                    }
                }



                else if (pointName == "Options" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedOptions, buttonsHoverLerpSpeed);
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 1f, 0.5f, 1f);
                    if (MousePress)
                    {


                        optionOpenMenu();
                     

                    }

                }



                else if (pointName == "Tutorial" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedTutorial, buttonsHoverLerpSpeed);
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 1f, 0.5f, 1f);

                    if (MousePress)
                    {

                        SceneManager.LoadScene("Tutorial1");

                    }
                }

                else
                {

                    transform.position = Vector3.Lerp(transform.position, spawn, buttonsHoverLerpSpeed);
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 1f, 1f, 1f);

                }

            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, spawn, buttonsHoverLerpSpeed);
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 1f, 1f);
            }

            /*   if (FirstTouch == false)
               {

                   SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                   renderer.color = new Color(1f, 1f, 1f, 0.1f);
               }
              */


        }
    }

    private void OnMouseEnter()
    {
        MouseHover = true;
        FirstTouch = true;
       
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

    private void optionOpenMenu()
    {

        Options.SetActive(true);
        Background.SetActive(true);
        Time.timeScale = 0f;
       

    }

}
