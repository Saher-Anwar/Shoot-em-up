using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float enemyHealth;
    public float enemyDamage;
    public float enemySize;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReduceHealth(float damage)
    {
        enemyHealth -= damage;
        Debug.Log($"Enemy health {enemyHealth}");

        if (enemyHealth <= 0)
        {
            EnemyDeath();
            animator.SetBool("Death", true);
        }
        else
        {
            animator.SetBool("Death", false);
        }
    }

    public void EnemyDeath()
    {
        // increase kill count
        // play sound effect
        // player particle effect

        Destroy(gameObject);
    }
}
