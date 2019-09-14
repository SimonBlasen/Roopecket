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

    public MainMenuText mainMenuTextRef;


    private Resolution oldRes;
    private int oldQualityIndex = 0;
    private float oldVolume = 0f;
    private bool oldIsFullscreen = false;
    private Resolution newRes;
    private int newQualityIndex = 0;
    private float newVolume = 0f;
    private bool newIsFullscreen = false;


    private void Start()
    {
        init();
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
        //QualitySettings.SetQualityLevel(oldQualityIndex);
        //Screen.fullScreen = oldIsFullscreen;
        //AudioListener.volume = oldVolume;
        //Screen.SetResolution(oldRes.width, oldRes.height, Screen.fullScreen);

        MenuOpened(false);
    }

}
