using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class GameUIHandler : MonoBehaviour
    {
        // UI Components
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Text pauseMenuText;
        [SerializeField] private GameObject gameOverUi;
        

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Run methods
            PauseGame();
            GameOver();

            scoreText.text = "Score: " + GameManager.Instance.gameScore;
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

        private void GameOver()
        {
            if (GameManager.Instance.gameRunning == false)
            {
                gameOverUi.SetActive(true);
            }
        }
    }
}
