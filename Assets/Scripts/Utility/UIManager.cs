using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
