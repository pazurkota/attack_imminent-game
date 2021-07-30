using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

public class PlayerController : MonoBehaviour
{
    // Private variables
    [SerializeField] private float speed; // Player speed
    [SerializeField] private float playerHealthPoints; // Player health
    [SerializeField] private bool canShoot = true; // Check if player can shoot
    private float _horizontalInput;
    private float _verticalInput;
    
    // Public variables
    public bool gameOver = false; // Check if game is NOT over
    
    // Private components
    private AudioSource _playerAudio;
    
    // Public components
    public GameObject bulletProjectile; 
    public ParticleSystem explosionFX;
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip criticalCondition;
    public AudioClip gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        _playerAudio = GetComponent<AudioSource>(); // Get AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        // Move player in X axis (horizontal)
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * _horizontalInput * Time.deltaTime);
        
        // Move player in Z axis (vertical)
        _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * _verticalInput * Time.deltaTime);
        
        PlaneShoot(); // Player cannon can shoot bullets in every 0.5 seconds
        KeepPlayerInBounds(); // Keep player inbounds
    }

    void PlaneShoot()
    {
        if (canShoot && !gameOver)
        {
            Instantiate(bulletProjectile, transform.position, bulletProjectile.transform.rotation);
            _playerAudio.PlayOneShot(shootSound, 1.0f);
            canShoot = false;
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

    IEnumerator ShootTimeout() // Wait 0.5 seconds to shoots
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && playerHealthPoints > 0)
        {
            playerHealthPoints--;
            Destroy(other.gameObject);

            if (playerHealthPoints == 1)
            {
                _playerAudio.PlayOneShot(criticalCondition, 1.0f);
            }
            else if (playerHealthPoints == 0)
            {
                Debug.Log("Game Over!");
                
                explosionFX.Play();
                
                Destroy(gameObject);
                
                _playerAudio.PlayOneShot(explosionSound, 1.0f);
                _playerAudio.PlayOneShot(gameOverSound, 1.0f);

                gameOver = true;
            }
        }
    }
}