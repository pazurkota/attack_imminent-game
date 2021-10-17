using System;
using UnityEngine;

namespace Player
{
    public class BulletManager : MonoBehaviour
    {
        // Variables
        [SerializeField] private float speed; // Bullet speed
        [SerializeField] private float maxBound; // Set the bullet max bound
    
        // Actual Code
        void Update()
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime); // Change bullet position  
        
            DestroyOutOfBounds(); // Destroy bullet if it fly out of the bounds
        }

        void DestroyOutOfBounds()
        {
            if (transform.position.z > maxBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
