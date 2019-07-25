using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider sliderVolume;
    public Toggle toggleFullscreen;

    public MainMenuText mainMenuTextRef;


    private Resolution oldRes;
    private int oldQualityIndex = 0;
    private float oldVolume = 0f;
    private bool oldIsFullscreen = false;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            resolutionDropdown.AddOptions(options);

            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void SetResolution(int resolutionIndex)
    {

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        AudioListener.volume = volume;
        //audioMixer.SetFloat("volume", volume);

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
            oldVolume = AudioListener.volume;
            oldIsFullscreen = Screen.fullScreen;
            oldRes = Screen.currentResolution;

            sliderVolume.value = oldVolume;
            toggleFullscreen.isOn = oldIsFullscreen;
            qualityDropdown.value = oldQualityIndex;

            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
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
        MenuOpened(false);
    }

    public void ButtonDiscard()
    {
        QualitySettings.SetQualityLevel(oldQualityIndex);
        Screen.fullScreen = oldIsFullscreen;
        AudioListener.volume = oldVolume;
        Screen.SetResolution(oldRes.width, oldRes.height, Screen.fullScreen);

        MenuOpened(false);
    }

}
