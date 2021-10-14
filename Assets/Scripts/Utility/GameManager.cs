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
        public AudioClip gameMusic;
        public AudioMixer playerMixer;
        public AudioMixer musicMixer;

        // Private Components
        private PlayerController _playerController;
        private AudioSource _cameraAudioSource;

        // Variables
        public int gameScore;
        public int highScore;
        public bool gameRunning;
        public bool isGamePaused;

        public static GameManager Instance;
        
        private void Awake() // Access GameManager without giving a reference
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Actual Code
        void Start()
        {
            Debug.Log("Game has been activated");
            _cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>(); // Get camera Audio Source component
            
            InvokeRepeating("EnemyPlaneSpawn", 3, Random.Range(2, 4));
            InvokeRepeating("EnemyHelicopterSpawn", 7, Random.Range(5, 10));
        }

        private void Update()
        {
            SaveHighScore();
            SetDeltaTime();
        }

        void SpawnPowerups()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8.45f, 8.45f), 6, Random.Range(-4.4f, 4.4f));
            int randomPowerup = Random.Range(0, powerupPrefab.Length);
            Instantiate(powerupPrefab[randomPowerup], spawnPos, powerupPrefab[randomPowerup].transform.rotation);
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
                Vector3 spawnPos = new Vector3(Random.Range(-8.1f, 8.1f), 6, 8);
                Instantiate(enemyHelicopterPrefab, spawnPos, enemyHelicopterPrefab.transform.rotation);   
            }
        }

        public void AddScore(int scoreToAdd)
        {
            gameScore += scoreToAdd;
            // scoreText.text = "Score: " + gameScore;
        }

        public void GameOver()
        {
            gameRunning = false;
            _cameraAudioSource.Stop();

            // gameOver.gameObject.SetActive(true);
            int score = PlayerPrefs.GetInt("Highscore");
            // highScoreText.text = "Highscore: " + score;
        }

        void SaveHighScore()
        {
            if (gameScore > highScore)
            {
                highScore = gameScore;
                PlayerPrefs.SetInt("Highscore", highScore);
            }
        }

        public void PauseGame()
        {
            isGamePaused = !isGamePaused;
        }

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