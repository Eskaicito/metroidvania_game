using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public PlayerHealthData playerHealthData;

    private void Start()
    {
       
        playerHealthData.InitializeValues();
    }

    public void Heal(int amount)
    {
        playerHealthData.currentHealth += amount;

        if (playerHealthData.currentHealth > playerHealthData.maxHealth)
        {
            playerHealthData.currentHealth = playerHealthData.maxHealth;
        }

        //Debug.Log("Salud del jugador: " + playerHealthData.currentHealth);
    }

    public void TakeDamage(int amount)
    {
        playerHealthData.currentHealth -= amount;

        if (playerHealthData.currentHealth < 0)
        {
            playerHealthData.currentHealth = 0;
        }

        //Debug.Log("Salud del jugador: " + playerHealthData.currentHealth);
    }

    
    public void UseEnergy(int amount)
    {
        playerHealthData.currentEnergy -= amount;

        if (playerHealthData.currentEnergy < 0)
        {
            playerHealthData.currentEnergy = 0;
        }

        //Debug.Log("Energía del jugador: " + playerHealthData.currentEnergy);
    }

    public void RestoreEnergy(int amount)
    {
        playerHealthData.currentEnergy += amount;

        if (playerHealthData.currentEnergy > playerHealthData.maxEnergy)
        {
            playerHealthData.currentEnergy = playerHealthData.maxEnergy;
        }

        //Debug.Log("Energía del jugador: " + playerHealthData.currentEnergy);
    }
}
