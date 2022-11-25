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

    private float currentHealth;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("heartItem"))
        {
            Destroy(other.gameObject);
            Debug.Log("heart collider");
            AddHealth(20.0f);
        }
        if (other.gameObject.CompareTag("staminaItem"))
        {
            Destroy(other.gameObject);
            Debug.Log("stamina collider");
            AddStamina(20.0f);
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

    public void AddHealth(float healthIncrease)
    {
        // if currentHealth + healthIncrease is more than maxhealth, then current health becomes maxHealt; otherwise, currenthealth = currentHealth + healthIncrease 
        currentHealth = (currentHealth + healthIncrease) > maxHealth ? maxHealth : currentHealth + healthIncrease;
        healthBar.setHealth(currentHealth);
    }

    public void AddStamina(float staminaIncrease)
    {
        // if currentStamina + staminaIncrease is more than maxStamina, then currentStamina becomes maxStamina; otherwise, currenthealth = currentStamina + staminaIncrease
        currentStamina = (currentStamina + staminaIncrease) > maxStamina ? maxStamina : currentStamina + staminaIncrease;
        staminaBar.SetCurrentStamina(currentStamina);
    }


}