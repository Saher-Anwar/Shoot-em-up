using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float enemyHealth;
    public float enemyDamage;
    public float enemySize;
    public int kills;
    [SerializeField] Text killCountText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReduceHealth(float damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        // increase kill count
        // play sound effect
        // player particle effect
        kills++;
        killCountText.text = ""+kills;
        Destroy(gameObject);
    }
}
