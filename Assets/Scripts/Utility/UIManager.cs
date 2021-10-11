using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

public class UIManager : MonoBehaviour
{   

    // Other Components
    public ScriptableObject PlayerController;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.gameRunning = true;
    }
}
