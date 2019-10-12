using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuText : MonoBehaviour {

    private RectTransform menuText;
    private Vector3 spawn;
    private Vector3 selectedGarage, selectedContinue, selectedManual, selectedOptions, selectedBack3, selectedBack2, selectedTutorial, selectedQuit, selectedMultiplayer, selectedSplitscreen, selectedOnline, selectedBack1, selectedTestarea, selectedfrageZeichen;
    private bool MouseHover = false;
    private bool Entered;
    private bool MousePress = false;
    private bool frageZeichenOpen;
    public bool FirstTouch = false;
    private bool optionsOpened;
    public string pointName;
    public Vector3 hidedButton;
    private float buttonsHoverLerpSpeed = 0.05f;

    [SerializeField]
    private MainMenuCam mainMenuCam;

    public GameObject Options;
    public GameObject Background;
    public GameObject FragezeichenText;

    // Use this for initialization
    void Start () {
        MouseHover = false;
        spawn = transform.position;

        selectedManual = new Vector3(0f, 0.25f, -4.15f);
        selectedContinue = new Vector3(0f, 0.05f, -4.15f);
        selectedGarage = new Vector3(0f, 0.15f, -4.15f);
        selectedOptions = new Vector3(0f, -0.15f, -4.15f);
        selectedTutorial = new Vector3(0f, -0.25f, -4.15f);
        selectedTestarea = new Vector3(0f, -0.35f, -4.15f);
        selectedQuit = new Vector3(0f, -0.8f, -4f);
        selectedfrageZeichen = new Vector3(0f, 0.75f, -4.05f);
        selectedMultiplayer = new Vector3(0f, -0.05f, -4.15f);
        selectedSplitscreen = new Vector3(1.791f, 0.05f, -4.63f);
        selectedOnline = new Vector3(1.791f, 0.137f, -4.63f);
        selectedBack1 = new Vector3(0f, 8.242f, -2.012f);
        selectedBack2 = new Vector3(1.791f, -0.25f, -4.63f);
        selectedBack3 = new Vector3(0f, -3.3f, -4.37f);

        hidedButton = new Vector3(100f, 100f, 100f);
        //FirstTouch = false;
       
           
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            if(pointName != "Quit" && pointName != "Back1")
            renderer.color = new Color(1f, 1f, 0.5f, 0.05f);



    }

    private float colorLerp = 0f;

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.GetInstanceID() == transform.GetInstanceID())
                        {
                            MouseHover = true;
                            MousePress = true;
                        }
                        else
                        {
                            MouseHover = false;
                            MousePress = false;
                        }
                    }
                    else
                    {
                        MouseHover = false;
                        MousePress = false;
                    }
                }
            }
        }





        if (FirstTouch)

        {
            if (colorLerp < 1f)
            {
                colorLerp += Time.deltaTime * 0.1f;
                if (colorLerp > 1f)
                {
                    colorLerp = 1f;
                }
            }
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            //Debug.Log(pointName + " " + colorLerp.ToString());


            if (MouseHover)
            {
                if (pointName == "Continue" && MouseHover)
                {

                    

                    transform.position = Vector3.Lerp(transform.position, selectedContinue, buttonsHoverLerpSpeed);
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                   

                    if (MousePress)
                    {
                       
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Singleplayer");
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 2;
                        //SceneManager.LoadScene("ComicScene");

                    }

                }

               else if (pointName == "Manual" && MouseHover)
                {



                    transform.position = Vector3.Lerp(transform.position, selectedManual, buttonsHoverLerpSpeed);
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);



                    if (MousePress)
                    {

                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 4;
                        //SceneManager.LoadScene("ComicScene");

                    }

                }



                else if (pointName == "Garage" && MouseHover)
                {

                   

                    transform.position = Vector3.Lerp(transform.position, selectedGarage, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {

                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        SceneManager.LoadScene("Garage");

                    }
                }

                else if (pointName == "Quit" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedQuit, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Application.Quit();

                    }
                }



                else if (pointName == "Options" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedOptions, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);
                    if (MousePress)
                    {
                        

                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        optionOpenMenu();
                     

                    }

                }



                else if (pointName == "Tutorial" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedTutorial, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        SceneManager.LoadScene("Tutorial1");

                    }
                }

                else if (pointName == "Testarea" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedTestarea, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        SceneManager.LoadScene("TestArea");

                    }
                }

                else if (pointName == "Fragezeichen" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedfrageZeichen, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        frageZeichenOpenMenu();



                    }
                }



                else if (pointName == "Multiplayer" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedMultiplayer, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Multiplayer");
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 1;

                    }
                }



                else if (pointName == "Splitscreen" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedSplitscreen, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Multiplayer");
                        SceneManager.LoadScene("Garage PvP");
                        //mainMenuCam.Index = 0;

                    }
                }

                else if (pointName == "Online" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedOnline, buttonsHoverLerpSpeed);
                    // SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Multiplayer");
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 0;

                    }
                }




                else if (pointName == "Back1" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedBack1, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    //renderer.color = new Color(1f, 1f, 0.5f, 1f);
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Back");
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 0;

                    }
                }




                else if (pointName == "Back2" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedBack2, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    //renderer.color = new Color(1f, 1f, 0.5f, 1f);
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Back");
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 0;

                    }
                }

                else if (pointName == "Back3" && MouseHover)
                {
                    transform.position = Vector3.Lerp(transform.position, selectedBack3, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    //renderer.color = new Color(1f, 1f, 0.5f, 1f);
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 0.5f, 1f), colorLerp);

                    if (MousePress)
                    {
                        FindObjectOfType<AudioManager>().Play("MenuButton");
                        Debug.Log("Back");
                        //SceneManager.LoadScene("Tutorial1");
                        mainMenuCam.Index = 0;

                    }
                }



                else
                {

                    transform.position = Vector3.Lerp(transform.position, spawn, buttonsHoverLerpSpeed);
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 1f, 1f), colorLerp);

                }

            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, spawn, buttonsHoverLerpSpeed);
                renderer.color = Color.Lerp(renderer.color, new Color(1f, 1f, 1f, 1f), colorLerp);
                //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                //renderer.color = new Color(1f, 1f, 1f, 1f);
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

    private void OnMouseUp()
    {
        MousePress = false;
    }

    private void OnMouseExit()
    {
        MouseHover = false;
    }

    private void optionOpenMenu()
    {
        OptionsMenuOpened(true);
    }

    private void frageZeichenOpenMenu()
    {
        frageZeichenMenuOpened(true);
    }

    public void OptionsMenuOpened(bool isOpen)
    {
        if (isOpen)
        {
            Options.SetActive(true);
            Options.GetComponent<SettingsMenu>().MenuOpened(true);
            Background.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Options.SetActive(false);
            Background.SetActive(false);
            Time.timeScale = 1f;
        }
    }

   

    public void frageZeichenMenuOpened(bool opened)
    {
        if (opened)
        {
            FragezeichenText.SetActive(true);
            
        }
        else
        {
            FragezeichenText.SetActive(false);
            
        }
    }

    

}
