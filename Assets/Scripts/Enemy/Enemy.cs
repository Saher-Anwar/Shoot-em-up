using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float enemyHealth;
    public float enemySize;
    Animator animator;
    public float hitDelay = 4f;

    private float enemyDamage;
    private GameManager gameManager;
    private bool isEnemyDeathCalled = false;
    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<NavMeshAgent>().speed = Random.Range(4, 8);
        enemyDamage = Random.Range(10, 40);
    }

    public void ReduceHealth(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            animator.SetBool("Death", true);
            StartCoroutine(EnemyDeath(animator.GetCurrentAnimatorStateInfo(0).length));
            isEnemyDeathCalled = true; // bool to avoid calling this method more than ocne
        }
        else
        {
            animator.SetBool("Death", false);
        }
    }


    public void DoDamage()
    {
        GameObject player = gameObject.GetComponent<EnemyTarget>().target;
        float distanceToDoDamage = gameObject.GetComponent<NavMeshAgent>().stoppingDistance + .5f;

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= distanceToDoDamage)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > hitDelay)
            {
                player.GetComponent<Character>().ReduceHealth(enemyDamage);
                elapsedTime = 0f;
            }
        }
    }

    IEnumerator EnemyDeath(float seconds)
    {
        if (!isEnemyDeathCalled)
        {
            Debug.Log("Enemy dead");
            // increase kill count
            gameManager.IncreaseKillCount();


            // play sound effect
            // player particle effect

            gameObject.GetComponent<EnemyMovement>().enabled = false;
            yield return new WaitForSeconds(seconds + 1f);

            Destroy(gameObject);
        }
        else
        {
            yield return null;
        }


    }
}
