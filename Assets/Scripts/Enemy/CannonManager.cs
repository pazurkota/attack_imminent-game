using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    private GameObject target;
    public GameObject cannonPart;
    public GameObject cannonProjectile;
    private bool _canShoot = true;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 4 * Time.deltaTime);
        
        ShootCannon();
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
