using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Note: Number of enemies killed should be tracked by the game manager 
 */
public class Character : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    public float defence = 0;
    public float attackPower = 0f;

    public float staminaReduction = 1f;
    public float staminaRegen = 1f;

    private float currentHealth;
    public float currentStamina;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float damage)
    {
        currentHealth -= (damage - defence);
    }

    public void ReduceStamina(float staminaReduction)
    {
        currentStamina -= staminaReduction;
    }

    public void ReduceStamina()
    {
        currentStamina -= staminaReduction;
        currentStamina = currentStamina < 0 ? 0 : currentStamina;
    }

    public void RegenStamina()
    {
        currentStamina += staminaRegen;
        currentStamina = currentStamina > maxStamina ? maxStamina : currentStamina;
    }
}
