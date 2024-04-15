using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// Based on Brackeys Unity settings tutorial released in 2018
    /// https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=26s&ab_channel=Brackeys
    /// </summary>


    // Start is called before the first frame update
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); // clear all options 
        List<string> options = new List<string>(); // creating list of strings for options
        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++) // loop through each element in resolution array
        {
            string option = resolutions[i].width + "x" + resolutions[i].height; // for each, formatted stirng to display resoultion 
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resolutionDropdown.AddOptions(options); // add option list to dropdown
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
    }
    // volume slider, uses audio mixer
    public void setVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }
    // graphic settings
    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }
    // full screen settings
    public void setFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    // resolution settings

    public void setResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
