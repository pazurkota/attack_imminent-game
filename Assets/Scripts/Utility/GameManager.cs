using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Utility
{
    public class GameManager : MonoBehaviour
    {
        // Public Components
        public GameObject enemyPlanePrefab;
        public GameObject enemyHelicopterPrefab;
        public GameObject[] powerupPrefab; // Get all power ups 

        // Private Components
        private PlayerController _playerController;
        private AudioSource _cameraAudioSource;

        // Variables
        public int gameScore;
        public int highScore;
        public bool gameRunning;
        public bool isGamePaused;

        public static GameManager Instance;
        
        // Access GameManager without giving a reference
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

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"[GAME] Game has been started (current scene opened: {SceneManager.GetActiveScene().name}.unity)"); // Show if game has started + get scene name
            _cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>(); // Get camera Audio Source component
            
            // InvokeRepeating 
            InvokeRepeating(nameof(EnemyPlaneSpawn), 3, Random.Range(2, 4));
            InvokeRepeating(nameof(EnemyHelicopterSpawn), 7, Random.Range(5, 10));
            InvokeRepeating(nameof(SpawnPowerups), 10, Random.Range(10, 25));
        }
        
        // Update is called once per frame
        private void Update()
        {
            SaveHighScore();
            SetDeltaTime();
        }
        
        // Spawn Power-ups during the game
        void SpawnPowerups()
        {
            if (gameRunning)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8.45f, 8.45f), 6, Random.Range(-4.4f, 4.4f));
                int randomPowerup = Random.Range(0, powerupPrefab.Length);
                Instantiate(powerupPrefab[randomPowerup], spawnPos, powerupPrefab[randomPowerup].transform.rotation);
            }
        }
        
        // Spawn the plane (enemy)
        void EnemyPlaneSpawn()
        {
            if (gameRunning) // Check if game is running
            {
                Vector3 spawnPos = new Vector3(Random.Range(-7.75f, 7.75f), 6, 8); 
                Instantiate(enemyPlanePrefab, spawnPos, enemyPlanePrefab.transform.rotation);
            }
        }

        // Spawn the helicopter (enemy)
        void EnemyHelicopterSpawn()
        {
            if (gameRunning) // Check if game is running
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8.1f, 8.1f), 6, 8);
                Instantiate(enemyHelicopterPrefab, spawnPos, enemyHelicopterPrefab.transform.rotation);   
            }
        }

        // Add score
        public void AddScore(int scoreToAdd)
        {
            gameScore += scoreToAdd;
        }

        // This method is activated after the player dies
        public void GameOver()
        {
            gameRunning = false;
            _cameraAudioSource.Stop();
        }

        // Save the highscore 
        void SaveHighScore()
        {
            if (gameScore > highScore)
            {
                highScore = gameScore;
                PlayerPrefs.SetInt("Highscore", highScore);
            }
        }
        
        // Pause game method (sets the "isGamePaused") bool
        public void PauseGame()
        {
            isGamePaused = !isGamePaused;
        }
        
        // Set the DeltaTime in game
        public void SetDeltaTime()
        {
            if (isGamePaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}