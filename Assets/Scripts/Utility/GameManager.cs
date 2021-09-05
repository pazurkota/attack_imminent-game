using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Utility
{
    public class GameManager : MonoBehaviour
    {
        // GUI Components
        public GameObject settings;
        public GameObject gameTitle;
        public GameObject gameOver;
        public GameObject gameStats;
        public GameObject pauseMenu;
        public TextMeshProUGUI scoreText;
        public Slider playerVolumeSlider;
        public Slider musicVolumeSlider;
        
        // GUI Animators
        [FormerlySerializedAs("_gameOverAnimator")] public Animator gameOverAnimator;
        
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
        public bool gameRunning;
        public bool isGamePaused;
        private static readonly int IsGameOver = Animator.StringToHash("isGameOver");

        // Actual Code
        void Start()
        {
            Debug.Log("Game has been activated");
            
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Find player and get PlayerController.cs script
            _cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>(); // Get camera Audio Source component

            InvokeRepeating("SpawnPowerups", 10, Random.Range(15, 23));
            InvokeRepeating("EnemyHelicopterSpawn", 10, Random.Range(10, 17));
            InvokeRepeating("EnemyPlaneSpawn", 2, Random.Range(4, 6));
            
            playerVolumeSlider.value = PlayerPrefs.GetFloat("PlayerVolume", 0.75f);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }

        private void Update()
        {
            PauseMenu();
        }

        void SpawnPowerups()
        {
            if (gameRunning)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8.45f, 8.45f), 6, Random.Range(-4.4f, 4.4f));
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
                Vector3 spawnPos = new Vector3(Random.Range(-8.1f, 8.1f), 6, 8);
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
            gameOverAnimator.SetBool(IsGameOver, true);
            
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

        public void OpenSettings()
        {
            settings.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
            gameTitle.gameObject.SetActive(false);
        }

        public void CloseSettings()
        {
            settings.gameObject.SetActive(false);

            if (isGamePaused)
            {
                pauseMenu.gameObject.SetActive(true);
            }
            else
            {
                gameTitle.gameObject.SetActive(true);
            }
        }

        public void HideShowStats()
        {
            if (gameStats != null)
            {
                bool isActive = gameStats.activeSelf;
                gameStats.SetActive(!isActive);
            }
        }

        public void SetPlayerVolume(float sliderValue)
        {
            playerMixer.SetFloat("PlayerVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("PlayerVolume", sliderValue);
        }

        public void SetMusicVolmue(float sliderValue)
        {
            musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }

        private void PauseMenu()
        {
            if (gameRunning && Input.GetKeyDown(KeyCode.Escape))
            {
                isGamePaused = !isGamePaused;
                PauseSettings();
            }
        }

        private void PauseSettings()
        {
            if (isGamePaused)
            {
                pauseMenu.gameObject.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0.0f;
            }
            else if(settings.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}