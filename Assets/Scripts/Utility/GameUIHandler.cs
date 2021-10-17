using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
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
            // Run methods
            PauseGame();
        }
        
        // Method that pause the game
        private void PauseGame()
        {
            if (Input.GetKeyDown(KeyCode.P)) // Check if "P" button has been pressed
            {
                GameManager.Instance.PauseGame(); // Pause the game
                pauseMenuText.gameObject.SetActive(GameManager.Instance.isGamePaused); // Show the "Game Paused" button
            }
        }
    }
}
