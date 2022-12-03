using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float waitBeforeLoseScreen = 1.5f;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameObject weaponContainerReference;
    public GameObject initialGunReference;
    public GameObject grenadeLauncher;
    public GameObject multibulletGun;
    public GameObject bloodEffect;

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
        if (other.gameObject.tag.Equals("Gun"))
        {
            initialGunReference.SetActive(false);
            Destroy(other.gameObject);

            if (grenadeLauncher != null)
            {
                GameObject newGrenadeLauncher = Instantiate(grenadeLauncher, weaponContainerReference.transform);
                newGrenadeLauncher.GetComponent<GrenadeLauncher>().enabled = true;
            }
        }
        if(other.gameObject.tag.Equals("Multibullet Gun"))
        {
            initialGunReference.SetActive(false);
            Destroy(other.gameObject);

            if (grenadeLauncher != null)
            {
                GameObject newMultibulletGun = Instantiate(multibulletGun, weaponContainerReference.transform);
            }
        }
        if (other.gameObject.tag.Equals("AttackSpeedPowerUp"))
        {
            Destroy(other.gameObject);
            if (initialGunReference.activeInHierarchy)
            {
                initialGunReference.GetComponent<Gun>().timeBetweenFire /= 2;
            }
        }
        if (other.gameObject.tag.Equals("DefensePowerUp"))
        {
            Debug.Log("Increasing defense");
            Destroy(other.gameObject);
            IncreaseDefense(10);
        }
    }

    public void ReduceHealth(float damage)
    {
        if (damage - defence < 0) return;

        currentHealth -= (damage - defence);
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            PlayDeathEffects();
            StartCoroutine(LoseScreen());
        }
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

    public void IncreaseDefense(float increaseAmount)
    {
        defence += increaseAmount;
        Debug.Log("Increasing defense");
    }

    private void PlayDeathEffects()
    {
        GameObject particleEffect = Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

    IEnumerator LoseScreen()
    {
        yield return new WaitForSeconds(waitBeforeLoseScreen);
        SceneManager.LoadScene("LoseScreen");
    }
}