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
            
            InvokeRepeating("SpawnPowerups", 10, Random.Range(15, 23));
            
            InvokeRepeating("EnemyHelicopterSpawn", 10, Random.Range(10, 17));
            InvokeRepeating("EnemyPlaneSpawn", 2, Random.Range(4, 6));
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

        void EnemyPlaneSpawn()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-7.75f, 7.75f), 6, 8); 
            Instantiate(enemyPlanePrefab, spawnPos, enemyPlanePrefab.transform.rotation);
        }

        void EnemyHelicopterSpawn()
        {
            if (_playerController.gameOver == false)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8.5f, 8.5f), 6, 8);
                Instantiate(enemyHelicopterPrefab, spawnPos, enemyHelicopterPrefab.transform.rotation);
            }
        }
    }
}
