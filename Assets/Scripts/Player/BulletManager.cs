using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxBound;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);    
        
        DestroyOutOfBounds();
    }

    void DestroyOutOfBounds()
    {
        if (transform.position.z > maxBound)
        {
            Destroy(gameObject);
        }
    }
}
