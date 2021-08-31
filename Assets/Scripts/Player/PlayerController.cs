using System.Collections;
using TMPro;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Private variables
        [SerializeField] private float speed; // Player speed
        [SerializeField] private float playerHealthPoints; // Player health
        [SerializeField] private bool canShoot = true; // Check if player can shoot
        [SerializeField] private bool strongerShoot = false; // Check if player have "Power" powerup;
        [SerializeField] private bool hasShield = false;
        private float _horizontalInput;
        private float _verticalInput;
    
        // Public variables
        public bool gameOver; // Check if game is NOT over
    
        // Private components
        private AudioSource _playerAudio; // AudioSource component
        private GameManager _gameManager;

        // Public components
        public GameObject playerShield; // Get player shield
        public GameObject bulletProjectile; // Player bullet projectile prefab 
        public ParticleSystem explosionFX; // Explosion effect
        public AudioClip shootSound; // Player bullet shoot sound effect
        public AudioClip explosionSound; // Explosion sound effect
        public AudioClip criticalCondition; // Critical condition sound effect
        public AudioClip gameOverSound; // Game over sound effect
        public TextMeshProUGUI livesText; // Player lives track
        
        /*
         * End of variable, down below it's actual code
         * 
         * Copyright: Jazzcat studios, all rights reserved ©
         */
    
        void Start()
        {
            _playerAudio = GetComponent<AudioSource>(); // Get AudioSource component from player
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    
        void Update()
        {
            // Move player in X axis (horizontal)
            _horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * speed * _horizontalInput * Time.deltaTime);
        
            // Move player in Z axis (vertical)
            _verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.down * speed * _verticalInput * Time.deltaTime);

            playerShield.transform.position = transform.position;
        
            PlaneShoot(); // Player cannon can shoot bullets in every 0.5 seconds
            KeepPlayerInBounds(); // Keep player inbounds
            
            livesText.text = "Lives: " + playerHealthPoints;
        }

        void PlaneShoot()
        {
            if (canShoot && _gameManager.gameRunning) // Check if player can shoot and the game is STILL running
            {
                Instantiate(bulletProjectile, transform.position, bulletProjectile.transform.rotation); // Instantiate (create) bullet
                _playerAudio.PlayOneShot(shootSound, 1.0f); // Play sound effect
                canShoot = false; // Set "can shoot" bool to false
                StartCoroutine(ShootTimeout());
            }
        }

        void KeepPlayerInBounds()
        {
            if (transform.position.z < -4.44f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -4.44f);
            }
            else if (transform.position.z > 4.05f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 4.05f);
            }

            if (transform.position.x < -7.83f)
            {
                transform.position = new Vector3(-7.83f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > 7.83f)
            {
                transform.position = new Vector3(7.83f, transform.position.y, transform.position.z);
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
            if (strongerShoot)
            {
                yield return new WaitForSeconds(4);
                strongerShoot = false;
            }

            if (hasShield)
            {
                yield return new WaitForSeconds(4);
                hasShield = false;
                playerShield.gameObject.SetActive(false);
            }
        }

        public void CreateShip()
        {
            gameObject.transform.localScale = new Vector3(20, 20, 20);
            gameObject.transform.position = new Vector3(0, 6, 0);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("EnemyHelicopter") && playerHealthPoints > 0)
            {
                playerHealthPoints -= 2;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("EnemyPlane") && playerHealthPoints > 0)
            {
                playerHealthPoints--;
                Destroy(other.gameObject);
            }

            if (playerHealthPoints <= 1 && playerHealthPoints > 0)
            {
                _playerAudio.PlayOneShot(criticalCondition, 1.0f);
            }
             
            if (playerHealthPoints == 0)
            {
                _gameManager.GameOver();
                explosionFX.Play();
                _playerAudio.PlayOneShot(gameOverSound, 1.0f);
                gameObject.transform.localScale = new Vector3(0, 0, 0);
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
                if (playerHealthPoints < 6)
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
                hasShield = true;
                playerShield.gameObject.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdown());
            }

            if (other.CompareTag("EnemyBullet"))
            {
                --playerHealthPoints;
                Destroy(other.gameObject);
            }
        }
    }
}