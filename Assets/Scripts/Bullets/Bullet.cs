using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject hitEffect;
    public float destroyBulletWaitTime;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
         * This causes issues for multi bullet guns
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            return;
        }
        */
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            // reduce enemy's health if they get hit by the bullet
            collision.gameObject.GetComponent<Enemy>().ReduceHealth(damage);
            Debug.Log("Hit enemy");
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
