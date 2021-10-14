using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GameUIHandler : MonoBehaviour
{
    // UI Components
    [SerializeField] private Text pauseMenuText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.PauseGame();
        }
    }
}
