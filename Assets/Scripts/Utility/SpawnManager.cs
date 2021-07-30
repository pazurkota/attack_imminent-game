using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        InvokeRepeating("EnemyPlaneSpawn", 2, Random.Range(3, 5));
        InvokeRepeating("EnemyShipSpawn", 2, Random.Range(8, 12));
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
