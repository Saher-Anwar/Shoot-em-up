using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5f;
    public GameObject hitEffect;
    public float destroyBulletWaitTime = 5f;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            return;
        }

        if (collision.gameObject.tag.Equals("Enemey"))
        {
            // reduce enemy's health if they get hit by the bullet
            // collision.GetComponent<EnemyAI>().ReduceHealth(damage);
        }

        if(hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitEffect, 5f); // TODO: adjust this value to the length of the hit particle effect 
        }
        
        Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyBulletWaitTime);
        Destroy(gameObject);
    }
}
