using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class CannonManager : MonoBehaviour
    {
    
        // Variables
        private GameObject _target;
        public GameObject cannonPart; // Get cannon part
        public GameObject cannonProjectile; // Cannon projectile prefab
        private bool _canShoot = true; // Check if ship can shoot
    
        // Actual code
        void Start()
        {
            _target = GameObject.Find("Player"); // Find player as a target
        }
        
        void Update()
        {
            // Lock cannon at player
            Vector3 direction = _target.transform.position - transform.position; 
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 4 * Time.deltaTime);
        
            ShootCannon(); // Shoot the cannon
        }

        void ShootCannon()
        {
            if (_canShoot)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, 6, transform.position.z);
                Instantiate(cannonProjectile, spawnPos, cannonPart.transform.rotation);
                _canShoot = false;
                StartCoroutine(ShootTimeout());
            }
        }

        IEnumerator ShootTimeout()
        {
            yield return new WaitForSeconds(1);
            _canShoot = true;
        }
    }
}
