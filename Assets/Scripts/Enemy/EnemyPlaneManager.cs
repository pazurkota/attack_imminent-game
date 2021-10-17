using UnityEngine;
using Utility;

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
                GameManager.Instance.AddScore(5);
            }

            if (other.CompareTag("Shield"))
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("EnemyHelicopter") || other.gameObject.CompareTag("EnemyPlane"))
            {
                transform.position = new Vector3(Random.Range(-2, 2), transform.position.y, transform.position.z);
            }
        }
    }
}
