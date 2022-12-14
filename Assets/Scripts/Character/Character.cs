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
    public float staminaRegenWaitTime = 4f;

    public float currentHealth;
    public float currentStamina; // TODO: change to private 
    public STAMINA_STATE staminaState;
    private float elapsedTime = 0;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    
    public enum STAMINA_STATE
    {
        ReducingStamina,
        RegeningStamina,
        StandBy,
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        staminaState = STAMINA_STATE.StandBy;
        healthBar.setMaxHealth(maxHealth);
        staminaBar.setMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime += Time.deltaTime;


        if (staminaState == STAMINA_STATE.ReducingStamina)
        {
            ReduceStamina();
            elapsedTime = 0;

        }
        else if (staminaState == STAMINA_STATE.RegeningStamina)
        {
            // wait x seconds before beginning to regen stamina
            if (elapsedTime >= staminaRegenWaitTime)
            {
                RegenStamina();
            }
        }

    }


    public void ReduceHealth(float damage)
    {
        currentHealth -= (damage - defence);
        healthBar.setHealth(currentHealth);
    }

    public void ReduceStamina(float staminaReduction)
    {
        currentStamina -= staminaReduction;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void ReduceStamina()
    {
        currentStamina -= staminaReduction;
        currentStamina = currentStamina < 0 ? 0 : currentStamina;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenStamina()
    {
        currentStamina += staminaRegen;
        currentStamina = currentStamina > maxStamina ? maxStamina : currentStamina;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    void OnCollisionStay(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Enemy")
        {
            ReduceHealth(collision.gameObject.GetComponent<Enemy>().enemyDamage);
        }
    }
}
