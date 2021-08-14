using System.Linq;
using Player;
using UnityEngine;

namespace Utility
{
    public class SpawnManager : MonoBehaviour
    {
        // Variables
        public GameObject[] enemyPrefab; // Get all enemies 
        public GameObject[] powerupPrefab; // Get all power ups 
    
        private PlayerController _playerController;

        // Actual Code
        void Start()
        {

            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Find player and get PlayerController.cs script
        
            InvokeRepeating("EnemyPlaneSpawn", 2, Random.Range(3, 5)); // Invoke spawning plane
            InvokeRepeating("EnemyShipSpawn", 2, Random.Range(8, 12)); // Invoke spawning ship
            InvokeRepeating("SpawnPowerups", 2, Random.Range(10, 16));
        }

        void EnemyPlaneSpawn()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.8f, 9.8f), 6, 8);
            if (_playerController.gameOver == false)
            {
                Instantiate(enemyPrefab[0], spawnPos, enemyPrefab[0].transform.rotation);
            }
        }

        void EnemyShipSpawn()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.8f, 9.8f), 0.357f, 9f);
            if (_playerController.gameOver == false)
            {
                Instantiate(enemyPrefab[1], spawnPos, enemyPrefab[1].transform.rotation);
            }
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
