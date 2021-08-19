using System;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyPlaneManager : MonoBehaviour
    {
        public GameObject planePropeller;

        private GameManager _gameManager;
        
        void Update()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            
            planePropeller.transform.Rotate(Vector3.right * 3000 * Time.deltaTime);
            DestroyOutOfBounds();
        }

        private void DestroyOutOfBounds()
        {
            transform.Translate(Vector3.left * 3 * Time.deltaTime);

            if (transform.position.z < -7.25f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                
                _gameManager.AddScore(5);
            }

            if (other.CompareTag("Shield"))
            {
                Destroy(gameObject);
            }
        }
    }
}
