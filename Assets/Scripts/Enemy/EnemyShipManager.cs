using UnityEngine;

namespace Enemy
{
    public class EnemyShipManager : MonoBehaviour
    {
        // Variables
        [SerializeField] private float speed;
        [SerializeField] private float maxBound;
        [SerializeField] private float healthPoints;
    
        // Actual code
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
            // Run script modules
            DestroyOutOfBounds(); // Destroy enemy when go out of screen
        }

        void DestroyOutOfBounds()
        {
            if (transform.position.z < maxBound)
            {
                Destroy(gameObject);
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet") && healthPoints > 0) // If player bullets hits enemy and ship have more than 0 health points
            {
                healthPoints--; // Take 1 health point
                Destroy(other.gameObject); // Destroy bullet
                if (healthPoints == 0) // If ship have 0 health point, destroy enemy ship
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
