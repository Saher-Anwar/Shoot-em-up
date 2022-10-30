using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTip;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        bullet.transform.localEulerAngles = new Vector3(90f, bullet.transform.localEulerAngles.y, bullet.transform.localEulerAngles.z);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(gunTip.forward * bulletForce, ForceMode.Impulse);
    }
}
