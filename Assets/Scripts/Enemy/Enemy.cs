using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float enemyHealth;
    public float enemyDamage;
    public float enemySize;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
            Destroy(gameObject);
    }

    public void ReduceHealth(float damage)
    {
        enemyHealth -= damage;
    }
}
