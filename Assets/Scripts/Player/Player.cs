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
    }

    public void TakeDamage(int amount)
    {
        playerHealthData.currentHealth -= amount;

        if (playerHealthData.currentHealth < 0)
        {
            playerHealthData.currentHealth = 0;
        }
    }

    public void UseEnergy(int amount)
    {
        playerHealthData.currentEnergy -= amount;

        if (playerHealthData.currentEnergy < 0)
        {
            playerHealthData.currentEnergy = 0;
        }
    }

    public void RestoreEnergy(int amount)
    {
        playerHealthData.currentEnergy += amount;

        if (playerHealthData.currentEnergy > playerHealthData.maxEnergy)
        {
            playerHealthData.currentEnergy = playerHealthData.maxEnergy;
        }
    }

    public bool HasEnoughEnergy(int amount)
    {
        return playerHealthData.currentEnergy >= amount;
    }
}
