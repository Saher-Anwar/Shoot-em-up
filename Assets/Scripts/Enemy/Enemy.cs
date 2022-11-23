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
            animator.SetBool("Death", true);
            Debug.Log($"Death animation length: {animator.GetCurrentAnimatorStateInfo(0).length}");
            StartCoroutine(EnemyDeath(animator.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            animator.SetBool("Death", false);
        }
    }

    IEnumerator EnemyDeath(float seconds)
    {
        // increase kill count
        // play sound effect
        // player particle effect
        yield return new WaitForSeconds(seconds);
        Debug.Log("Enemy should die");
        Destroy(gameObject);
    }
}
