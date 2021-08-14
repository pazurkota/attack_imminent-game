using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxBound;
    [SerializeField] private float healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        // Run script modules
        DestoryOutOfBounds(); // Destroy enemy when go out of screen
    }

    void DestoryOutOfBounds()
    {
        if (transform.position.z < maxBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && healthPoints > 0)
        {
            --healthPoints;
            Destroy(other.gameObject);
            if (healthPoints == 0)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Shield") && healthPoints > 0)
        {
            --healthPoints;
            Destroy(gameObject);
        }
    }
}
