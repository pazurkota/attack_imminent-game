using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Enemy
{
    public class EnemyBulletManager : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * 20 * Time.deltaTime);

            if (transform.position.z < -5.3f)
            {
                Destroy(gameObject);
            }
        }
    }
}
