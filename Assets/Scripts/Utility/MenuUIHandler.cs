using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Utility;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

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
