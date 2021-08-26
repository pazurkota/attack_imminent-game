using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Utility
{
    public class GameManager : MonoBehaviour
    {
        // GUI Components
        public GameObject credits;
        public GameObject gameTitle;
        public GameObject gameOver;
        public GameObject gameStats;
        public TextMeshProUGUI scoreText;
        
        // Public Components
        public GameObject enemyPlanePrefab;
        public GameObject enemyHelicopterPrefab;
        public GameObject[] powerupPrefab; // Get all power ups 
        public AudioClip gameMusic;

        // Private Components
        private PlayerController _playerController;
        private AudioSource _cameraAudioSource;
        
        // Variables
        public int gameScore;
        public bool gameRunning;

        // Actual Code
        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Find player and get PlayerController.cs script
            _cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            
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
            _cameraAudioSource.Stop();
            
            gameOver.gameObject.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartGame()
        {
            gameRunning = true;
            _playerController.CreateShip();
            _cameraAudioSource.PlayOneShot(gameMusic, 1.0f);
            
            gameTitle.gameObject.SetActive(false);
        }

        public void OpenCredits()
        {
            credits.gameObject.SetActive(true);
            gameTitle.gameObject.SetActive(false);
        }

        public void CloseCredits()
        {
            credits.gameObject.SetActive(false);
            gameTitle.gameObject.SetActive(true);
        }

        public void HideShowStats()
        {
            if (gameStats != null)
            {
                bool isActive = gameStats.activeSelf;
                gameStats.SetActive(!isActive);
            }
        }
    }
}
