using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions = null;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public TMPro.TMP_Dropdown languageDropdown;
    public Slider sliderVolume;
    public Toggle toggleFullscreen;
    public TMPro.TextMeshProUGUI buttonKey1;
    public TMPro.TextMeshProUGUI buttonKey2;
    public TMPro.TextMeshProUGUI buttonKey3;
    public TMPro.TextMeshProUGUI buttonKey4;
    public TMPro.TextMeshProUGUI buttonKey5;
    public TMPro.TextMeshProUGUI buttonLandingMover;
    public TMPro.TextMeshProUGUI buttonReset;
    public TMPro.TextMeshProUGUI buttonSpecialLeft;
    public TMPro.TextMeshProUGUI buttonSpecialRight;

    public MainMenuText mainMenuTextRef;


    private Resolution oldRes;
    private int oldQualityIndex = 0;
    private float oldVolume = 0f;
    private bool oldIsFullscreen = false;
    private Resolution newRes;
    private int newQualityIndex = 0;
    private float newVolume = 0f;
    private bool newIsFullscreen = false;

    private int waitingForKeyIndex = -1;


    private void Start()
    {
        init();
    }

    private void Update()
    {
        if (waitingForKeyIndex != -1)
        {
            if (Input.anyKeyDown)
            {
                KeyCode key = KeyCode.A;
                for (int i = 0; i < 512; i++)
                {
                    if (Input.GetKeyDown((KeyCode)i))
                    {
                        key = (KeyCode)i;
                        break;
                    }
                }

                if (waitingForKeyIndex == 0)
                    Statics.key1 = key;
                else if (waitingForKeyIndex == 1)
                    Statics.key2 = key;
                else if (waitingForKeyIndex == 2)
                    Statics.key3 = key;
                else if (waitingForKeyIndex == 3)
                    Statics.key4 = key;
                else if (waitingForKeyIndex == 4)
                    Statics.key5 = key;
                else if (waitingForKeyIndex == 5)
                    Statics.keyLandingMovers = key;
                else if (waitingForKeyIndex == 6)
                    Statics.keyReset= key;
                else if (waitingForKeyIndex == 7)
                    Statics.keySpecialLeft = key;
                else if (waitingForKeyIndex == 8)
                    Statics.keySpecialRight = key;

                waitingForKeyIndex = -1;
                RefreshKeyButtons();
            }
        }
    }

    public void ButtonControlsKeyClicked(int buttonIndex)
    {
        if (waitingForKeyIndex == -1)
        {
            string waitForInputText = "...";
            waitingForKeyIndex = buttonIndex;
            if (waitingForKeyIndex == 0)
                buttonKey1.text = waitForInputText;
            else if (waitingForKeyIndex == 1)
                buttonKey2.text = waitForInputText;
            else if (waitingForKeyIndex == 2)
                buttonKey3.text = waitForInputText;
            else if (waitingForKeyIndex == 3)
                buttonKey4.text = waitForInputText;
            else if (waitingForKeyIndex == 4)
                buttonKey5.text = waitForInputText;
            else if (waitingForKeyIndex == 5)
                buttonLandingMover.text = waitForInputText;
            else if (waitingForKeyIndex == 6)
                buttonReset.text = waitForInputText;
            else if (waitingForKeyIndex == 7)
                buttonSpecialLeft.text = waitForInputText;
            else if (waitingForKeyIndex == 8)
                buttonSpecialRight.text = waitForInputText;
        }
        else
        {
            //waitingForKeyIndex = -1;
            //RefreshKeyButtons();
        }
    }

    public void RefreshKeyButtons()
    {
        buttonKey1.text = Statics.key1.ToString();
        buttonKey2.text = Statics.key2.ToString();
        buttonKey3.text = Statics.key3.ToString();
        buttonKey4.text = Statics.key4.ToString();
        buttonKey5.text = Statics.key5.ToString();
        buttonLandingMover.text = Statics.keyLandingMovers.ToString();
        buttonReset.text = Statics.keyReset.ToString();
        buttonSpecialLeft.text = Statics.keySpecialLeft.ToString();
        buttonSpecialRight.text = Statics.keySpecialRight.ToString();
    }

    public void SetResolution(int resolutionIndex)
    {

        //Resolution resolution = resolutions[resolutionIndex];
        // Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        newRes = resolutions[resolutionIndex];
    }

    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        //AudioListener.volume = volume;
        //audioMixer.SetFloat("volume", volume);

        newVolume = volume;
    }

    public void SetQuality(int qualityIndex)
    {

        newQualityIndex = qualityIndex;

        //QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen (bool isFullscreen)
    {
        newIsFullscreen = isFullscreen;
        //Screen.fullScreen = isFullscreen;
    }

    private KeyCode key1 = KeyCode.A;
    private KeyCode key2 = KeyCode.S;
    private KeyCode key3 = KeyCode.D;
    private KeyCode key4 = KeyCode.F;
    private KeyCode key5 = KeyCode.G;
    private KeyCode keyLandingMovers = KeyCode.Space;
    private KeyCode keyReset = KeyCode.R;
    private KeyCode keySpecialLeft = KeyCode.LeftArrow;
    private KeyCode keySpecialRight = KeyCode.RightArrow;

    private void init()
    {
        if (resolutions == null)
        {
            resolutions = new Resolution[8];
            resolutions[0] = new Resolution();
            resolutions[0].width = 640;
            resolutions[0].height = 480;
            resolutions[1] = new Resolution();
            resolutions[1].width = 800;
            resolutions[1].height = 600;
            resolutions[2] = new Resolution();
            resolutions[2].width = 1366;
            resolutions[2].height = 768;
            resolutions[3] = new Resolution();
            resolutions[3].width = 1600;
            resolutions[3].height = 900;
            resolutions[4] = new Resolution();
            resolutions[4].width = 1920;
            resolutions[4].height = 1080;
            resolutions[5] = new Resolution();
            resolutions[5].width = 1920;
            resolutions[5].height = 1200;
            resolutions[6] = new Resolution();
            resolutions[6].width = 2560;
            resolutions[6].height = 1440;
            resolutions[7] = new Resolution();
            resolutions[7].width = 2560;
            resolutions[7].height = 1600;

            RefreshKeyButtons();

            List<Resolution> newRes = new List<Resolution>();
            newRes.AddRange(resolutions);

            for (int i = 0; i < newRes.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < Screen.resolutions.Length; j++)
                {
                    if (newRes[i].width == Screen.resolutions[j].width && newRes[i].height == Screen.resolutions[j].height)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    newRes.RemoveAt(i);
                    i--;
                }
            }

            resolutions = newRes.ToArray();

            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }

                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void SetLanguage(int index)
    {
        if (index == 0)
        {
            LanguageManager.Language = Language.ENGLISH;
        }
        else if (index == 1)
        {
            LanguageManager.Language = Language.GERMAN;
        }
        else
        {
            LanguageManager.Language = Language.SPANISH;
        }
    }

    Language oldLanguage = Language.ENGLISH;

    public void MenuOpened(bool open)
    {
        if (open)
        {
            init();

            key1 = Statics.key1;
            key2 = Statics.key2;
            key3 = Statics.key3;
            key4 = Statics.key4;
            key5 = Statics.key5;
            keyLandingMovers = Statics.keyLandingMovers;
            keyReset = Statics.keyReset;
            keySpecialLeft = Statics.keySpecialLeft;
            keySpecialRight = Statics.keySpecialRight;

            oldQualityIndex = QualitySettings.GetQualityLevel();
            oldVolume = AudioListener.volume;
            oldIsFullscreen = Screen.fullScreen;
            oldRes = Screen.currentResolution;
            newIsFullscreen = oldIsFullscreen;

            sliderVolume.value = oldVolume;
            toggleFullscreen.isOn = oldIsFullscreen;
            qualityDropdown.value = oldQualityIndex;
            Language language = LanguageManager.Language;
            oldLanguage = language;
            if (language == Language.ENGLISH)
            {
                languageDropdown.value = 0;
            }
            else if (language == Language.GERMAN)
            {
                languageDropdown.value = 1;
            }
            else if (language == Language.SPANISH)
            {
                languageDropdown.value = 2;
            }

            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].height == Screen.height && resolutions[i].width == Screen.width)
                {
                    resolutionDropdown.value = i;
                    break;
                }
            }

            RefreshKeyButtons();
        }
        else
        {
            mainMenuTextRef.OptionsMenuOpened(false);
        }
    }


    public void ButtonSave()
    {
        QualitySettings.SetQualityLevel(newQualityIndex);
        Screen.fullScreen = newIsFullscreen;
        AudioListener.volume = newVolume;
        Screen.SetResolution(newRes.width, newRes.height, newIsFullscreen);


        MenuOpened(false);
    }

    public void ButtonDiscard()
    {
        LanguageManager.Language = oldLanguage;

        Statics.key1 = key1;
        Statics.key2 = key2;
        Statics.key3 = key3;
        Statics.key4 = key4;
        Statics.key5 = key5;
        Statics.keyLandingMovers = keyLandingMovers;
        Statics.keyReset = keyReset;
        Statics.keySpecialLeft = keySpecialLeft;
        Statics.keySpecialRight = keySpecialRight;

        //QualitySettings.SetQualityLevel(oldQualityIndex);
        //Screen.fullScreen = oldIsFullscreen;
        //AudioListener.volume = oldVolume;
        //Screen.SetResolution(oldRes.width, oldRes.height, Screen.fullScreen);

        MenuOpened(false);
    }

}
