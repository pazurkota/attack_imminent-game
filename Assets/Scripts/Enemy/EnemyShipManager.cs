using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipManager : MonoBehaviour
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
        if (other.gameObject.CompareTag("Bullet") && healthPoints > 0)
        {
            healthPoints--;
            Destroy(other.gameObject);
            if (healthPoints == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
