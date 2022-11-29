using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTip;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public float timeBetweenFire = .5f;     // wait time before firing the next bullet
    public GameObject muzzleFlash;

    private new AudioSource audio;

    private float elapsedTime = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // player can only fire bullet once the timer has exceeded the wait time
        if(elapsedTime >= timeBetweenFire)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                elapsedTime = 0;
            }
        }
        
    }

    private void Shoot()
    {
        audio.PlayOneShot(audio.clip);
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        GameObject particleEffect = Instantiate(muzzleFlash, gunTip.position, Quaternion.identity);
        bullet.transform.localEulerAngles = new Vector3(0, bullet.transform.localEulerAngles.y, bullet.transform.localEulerAngles.z);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(gunTip.forward * bulletForce, ForceMode.Impulse);
    }
}
