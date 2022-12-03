
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBulletGun : MonoBehaviour
{
    public Transform gunTip;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public float timeBetweenFire;     // wait time before firing the next bullet

    private float elapsedTime = 0f;
    private new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // player can only fire bullet once the timer has exceeded the wait time
        if (elapsedTime >= timeBetweenFire)
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
        GameObject bullet;
        int angle = -60;
        for ( int i = 0; i<5; i++)
        {
            Vector3 vec = Quaternion.Euler(0, angle, 0) * gunTip.forward;

            bullet = Instantiate(bulletPrefab, gunTip.position + vec, gunTip.rotation * Quaternion.Euler(0, angle/2, 0));
            bullet.transform.localEulerAngles = new Vector3(0, bullet.transform.localEulerAngles.y , bullet.transform.localEulerAngles.z);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
            rb.AddForce(vec * bulletForce, ForceMode.Impulse);
            angle += 30;
        }
    }
}
