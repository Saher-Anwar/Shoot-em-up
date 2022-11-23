using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        // DoDamage();
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


    public void DoDamage()
    {
        GameObject player = gameObject.GetComponent<EnemyTarget>().target;
        float distanceToDoDamage = gameObject.GetComponent<NavMeshAgent>().stoppingDistance;

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= distanceToDoDamage)
        {
            player.GetComponent<Character>().ReduceHealth(enemyDamage);
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
