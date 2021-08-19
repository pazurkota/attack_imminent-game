using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyPlaneManager : MonoBehaviour
    {
        public GameObject planePropeller;
        
        void Update()
        {
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
            }

            if (other.CompareTag("Shield"))
            {
                Destroy(gameObject);
            }
        }
    }
}
