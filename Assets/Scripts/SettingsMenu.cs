using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    public MainMenuText mainMenuTextRef;


    private int oldQualityIndex = 0;
    private float oldVolume = 0f;
    private bool oldIsFullscreen = false;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {

            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            resolutionDropdown.AddOptions(options);

        }
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);

    }

    public void SetQuality(int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void MenuOpened(bool open)
    {
        if (open)
        {
            oldQualityIndex = QualitySettings.GetQualityLevel();
            audioMixer.GetFloat("volume", out oldVolume);
            oldIsFullscreen = Screen.fullScreen;
        }
        else
        {
            mainMenuTextRef.OptionsMenuOpened(false);
        }
    }


    public void ButtonSave()
    {
        MenuOpened(false);
    }

    public void ButtonDiscard()
    {
        QualitySettings.SetQualityLevel(oldQualityIndex);
        Screen.fullScreen = oldIsFullscreen;
        audioMixer.SetFloat("volume", oldVolume);

        MenuOpened(false);
    }

}
