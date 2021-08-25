using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility
{
    public class GameManager : MonoBehaviour
    {
        // GUI Components
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameOverText;
        public Button restartButton;
        
        // Public Variables
        public int gameScore;
        public bool gameRunning;

        // Variables
        public GameObject enemyPlanePrefab;
        public GameObject enemyHelicopterPrefab;
        public GameObject[] powerupPrefab; // Get all power ups 
        
        
        private PlayerController _playerController;

        // Actual Code
        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Find player and get PlayerController.cs script
            
            InvokeRepeating("SpawnPowerups", 10, Random.Range(15, 23));
            
            InvokeRepeating("EnemyHelicopterSpawn", 10, Random.Range(10, 17));
            InvokeRepeating("EnemyPlaneSpawn", 2, Random.Range(4, 6));
        }

        void SpawnPowerups()
        {
            if (gameRunning)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-9.8f, 9.8f), 6, Random.Range(-4.5f, 4.5f));
                int randomPowerup = Random.Range(0, powerupPrefab.Length);
                Instantiate(powerupPrefab[randomPowerup], spawnPos, powerupPrefab[randomPowerup].transform.rotation);
            }
        }

        void EnemyPlaneSpawn()
        {
            if (gameRunning)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-7.75f, 7.75f), 6, 8); 
                Instantiate(enemyPlanePrefab, spawnPos, enemyPlanePrefab.transform.rotation);
            }
        }

        void EnemyHelicopterSpawn()
        {
            if (gameRunning)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8.5f, 8.5f), 6, 8);
                Instantiate(enemyHelicopterPrefab, spawnPos, enemyHelicopterPrefab.transform.rotation);
            }
        }

        public void AddScore(int scoreToAdd)
        {
            gameScore += scoreToAdd;
            scoreText.text = "Score: " + gameScore;
        }

        public void GameOver()
        {
            gameRunning = false;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
