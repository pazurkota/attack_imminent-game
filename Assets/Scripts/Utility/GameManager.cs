using Player;
using UnityEngine;

namespace Utility
{
    public class GameManager : MonoBehaviour
    {
        // Variables
        public GameObject enemyPlanePrefab;
        public GameObject enemyHelicopterPrefab;
        
        public GameObject[] powerupPrefab; // Get all power ups 
    
        private PlayerController _playerController;

        // Actual Code
        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Find player and get PlayerController.cs script
            
            InvokeRepeating("SpawnPowerups", 2, Random.Range(10, 16));
        }

        void SpawnPowerups()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.8f, 9.8f), 6, Random.Range(-4.5f, 4.5f));
            int randomPowerup = Random.Range(0, powerupPrefab.Length);
            if (_playerController.gameOver == false)
            {
                Instantiate(powerupPrefab[randomPowerup], spawnPos, powerupPrefab[randomPowerup].transform.rotation);
            }
        }
    }
}
