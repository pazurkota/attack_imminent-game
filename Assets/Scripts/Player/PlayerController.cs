using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Private variables
        [SerializeField] private float speed; // Player speed
        [SerializeField] private float playerHealthPoints; // Player health
        [SerializeField] private bool canShoot = true; // Check if player can shoot
        [SerializeField] private bool strongerShoot = false; // Check if player have "Power" powerup;
        private float _horizontalInput;
        private float _verticalInput;
    
        // Public variables
        public bool gameOver; // Check if game is NOT over
    
        // Private components
        private AudioSource _playerAudio; // AudioSource component
    
        // Public components
        public GameObject bulletProjectile; // Player bullet projectile prefab 
        public GameObject playerShield; // Get player shield
        public ParticleSystem explosionFX; // Explosion effect
        public AudioClip shootSound; // Player bullet shoot sound effect
        public AudioClip explosionSound; // Explosion sound effect
        public AudioClip criticalCondition; // Critical condition sound effect
        public AudioClip gameOverSound; // Game over sound effect
        
        /*
         * End of variable, down below it's actual code
         * 
         * Copyright: Interactive studios, all rights reserved ©
         */
    
        void Start()
        {
            _playerAudio = GetComponent<AudioSource>(); // Get AudioSource component from player
        }
    
        void Update()
        {
            // Move player in X axis (horizontal)
            _horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * speed * _horizontalInput * Time.deltaTime);
        
            // Move player in Z axis (vertical)
            _verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * speed * _verticalInput * Time.deltaTime);

            playerShield.transform.position = transform.position;
        
            PlaneShoot(); // Player cannon can shoot bullets in every 0.5 seconds
            KeepPlayerInBounds(); // Keep player inbounds
        }

        void PlaneShoot()
        {
            if (canShoot && !gameOver) // Check if player can shoot and the game is NOT over
            {
                Instantiate(bulletProjectile, transform.position, bulletProjectile.transform.rotation); // Instantiate (create) bullet
                _playerAudio.PlayOneShot(shootSound, 1.0f); // Play sound effect
                canShoot = false; // Set "can shoot" bool to false
                StartCoroutine(ShootTimeout());
            }
        }

        void KeepPlayerInBounds()
        {
            if (transform.position.z < -4.2f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -4.2f);
            }
            else if (transform.position.z > 4.2f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 4.2f);
            }

            if (transform.position.x < -9.8f)
            {
                transform.position = new Vector3(-9.8f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > 9.8f)
            {
                transform.position = new Vector3(9.8f, transform.position.y, transform.position.z);
            }
            
        }

        IEnumerator ShootTimeout() 
        {
            if (strongerShoot)
            {
                yield return new WaitForSeconds(0.15f);
                canShoot = true;
            }
            else
            {
                yield return new WaitForSeconds(0.5f); // Wait 0.5 seconds to shoots
                canShoot = true; // Set "can shoot" bool to true
            }
        }

        IEnumerator PowerupCountdown()
        {
            yield return new WaitForSeconds(4);
            strongerShoot = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy") && playerHealthPoints > 0) // If gameObject has tag "Enemy" and player have more than 0 health points
            {
                playerHealthPoints--; // Take 1 player point
                Destroy(other.gameObject); // Destroy enemy

                if (playerHealthPoints == 1) // If player have the last health point
                {
                    _playerAudio.PlayOneShot(criticalCondition, 1.0f);
                }
                else if (playerHealthPoints == 0) // If player have 0 health points
                {
                    Debug.Log("Game Over!");
                 
                    explosionFX.Play(); // Play explosion FX

                    gameObject.transform.localScale = new Vector3(0, 0, 0); // Scale the player to 0
                
                    _playerAudio.PlayOneShot(explosionSound, 1.0f); // Play explosion sound effect
                    _playerAudio.PlayOneShot(gameOverSound, 1.0f); // Play "Game Over" sound effect

                    gameOver = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PowerPowerup"))
            {
                strongerShoot = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdown());
            }

            if (other.gameObject.CompareTag("RepairPowerup"))
            {
                if (playerHealthPoints < 4)
                {
                    ++playerHealthPoints;
                    Debug.Log($"Added 1 HP! Now you have {playerHealthPoints} HP!");
                }
                else
                {
                    Debug.Log("You have already max hp!");
                }
                Destroy(other.gameObject);
            }

            if (other.gameObject.CompareTag("ShieldPowerup"))
            {
                Debug.Log($"Collided with: {other.gameObject.name}");
                Destroy(other.gameObject);
            }
        }
    }
}