using UnityEngine;

namespace Enemy
{
    public class RocketManager : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            DestroyOutOfBounds(); // Destroy out of bounds
        }

        void DestroyOutOfBounds()
        {
            if (transform.position.z < -6.5f)
            {
                Destroy(gameObject);   
            }
        }
    }
}
