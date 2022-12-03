using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DynamicBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public AudioClip sfx;

    public float damage;
    public float damageRadius = 2f;
    public float destroyBulletWaitTime;
    public float bounceLimit = 3;
    [Range(10f, 100f)]
    public float bounciness = 30f;

    private float bounceCount = 0;
    private new Rigidbody rigidbody;
    private new AudioSource audio;

    private void Start()
    {
        bounceLimit *= 2;
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        StartCoroutine(DestroyBullet());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            // create radius and damage enemy within that radius

            // reduce enemy's health if they get hit by the bullet
            DoDamage();
            if (hitEffect != null) PlayEffects();

        }

        #region Bounciness Code
        bounceCount++;
        Debug.Log(bounceCount);
        if(bounceCount >= bounceLimit)
        {
            DoDamage();
            Destroy(gameObject);
            if (hitEffect != null) PlayEffects();
        }
        else
        {
            rigidbody.AddForce(new Vector3(0,bounciness,0), ForceMode.Impulse);
            Debug.Log("Adding upward force");
        }
        #endregion
    }

    private void DoDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag.Equals("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>()?.ReduceHealth(damage);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, damageRadius);  
    }

    private void PlayEffects()
    {
        GameObject particleEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        particleEffect.transform.parent = null;
       
        AudioSource audio = particleEffect.GetComponent<AudioSource>();

        #region Error Checking
        if (audio == null)
        {
            Debug.LogWarning("Particle effect game object does not have Audio Source component attached!");
            return;
        }

        if(sfx == null)
        {
            Debug.LogWarning("No sound effect attached!");
            return;
        }
        #endregion

        audio.clip = sfx;
        audio.PlayOneShot(audio.clip);
    }

    IEnumerator DestroyBullet()
    {
        // TODO: blow up and destroy bullet
        yield return new WaitForSeconds(destroyBulletWaitTime);
        Destroy(gameObject);
    }
}
