using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBullet : MonoBehaviour
{
    public float damage = 5f;
    public GameObject hitEffect;
    public float destroyBulletWaitTime = 5f;
    public PhysicMaterial bounceMaterial;
    public float bounciness = .5f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        if(bounceMaterial == null)
        {
            Debug.LogError("No bounce material attached. Either attach a bounce material or use regular Bullet script.");
        }

        bounceMaterial.bounciness = bounciness;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            return;
        }

        if (collision.gameObject.tag.Equals("Ground"))
        {
            return;
        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            // reduce enemy's health if they get hit by the bullet
            // collision.GetComponent<EnemyAI>().ReduceHealth(damage);
        }

        if (hitEffect != null)
        {
            PlayEffects();
        }

        Destroy(gameObject);
    }

    private void PlayEffects()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffect, 5f); // TODO: adjust this value to the length of the hit particle effect 
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyBulletWaitTime);
        Destroy(gameObject);
    }
}
