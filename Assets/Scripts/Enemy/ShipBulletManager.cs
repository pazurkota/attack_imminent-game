using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBulletManager : MonoBehaviour
{
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        DestroyOutOfBounds();
        KeepInBounds();
    }
    
    void DestroyOutOfBounds()
    {
        if (transform.position.z < -4.2f)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z > 4.2f)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -9.8f)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > 9.8f)
        {
            Destroy(gameObject);
        }
    }

    void KeepInBounds()
    {
        if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);            
        }
    }
}
