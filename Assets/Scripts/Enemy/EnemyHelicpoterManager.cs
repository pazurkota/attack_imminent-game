using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyHelicpoterManager : MonoBehaviour
    {
        public GameObject helicopterPropeller;
        public GameObject bulletProjectile;
        [SerializeField] private float healthPoints;

        private GameManager _gameManager;
        
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            
            InvokeRepeating("SpawnBullet", 2, 1.5f);
        }

        // Update is called once per frame
        void Update()
        {
            helicopterPropeller.transform.Rotate(Vector3.up * 3000 * Time.deltaTime);
            
            DestroyOutOfBounds();
        }

        private void SpawnBullet()
        {
            Instantiate(bulletProjectile, transform.position, bulletProjectile.transform.rotation);
        }

        private void DestroyOutOfBounds()
        {
            transform.Translate(Vector3.right * 2 * Time.deltaTime);

            if (transform.position.z < -7.35f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                --healthPoints;
                Destroy(other.gameObject);
                
                if (healthPoints == 0)
                {
                    Destroy(gameObject);
                    _gameManager.AddScore(10);
                }
            }
            
            if (other.CompareTag("Shield"))
            {
                Destroy(gameObject);
            }
        }
    }
}
