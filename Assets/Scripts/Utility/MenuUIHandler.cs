using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

public class MenuUIHandler : MonoBehaviour
{
    // Audio mixers
    [SerializeField] private AudioMixer musicVol;
    [SerializeField] private AudioMixer playerVol;
    
    // Sliders
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider playerVolSlider;
    
    // Main menu components
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    private void Start()
    {
        playerVolSlider.value = PlayerPrefs.GetFloat("PlayerVol", 0.75f);
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
    }

    public void SetPlayerVolume(float sliderValue)
    {
        playerVol.SetFloat("PlayerVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("PlayerVol", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        musicVol.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    public void OpenSettings()
    {
        if (mainMenu != null && settingsMenu != null)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (mainMenu != null && settingsMenu != null)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);   
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.gameRunning = true;
    }
    
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
