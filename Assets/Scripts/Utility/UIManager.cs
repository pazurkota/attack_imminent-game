using System;
using System.Collections;
using System.Collections.Generic;
using Player;
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
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
