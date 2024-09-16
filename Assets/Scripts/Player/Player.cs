using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    // Referencia al ScriptableObject
    public PlayerHealthData playerHealthData;

    private void Start()
    {
        // Si es la primera vez que se carga el jugador, aseguramos que su vida actual es igual a la máxima
        if (playerHealthData.currentHealth <= 0)
        {
            playerHealthData.currentHealth = playerHealthData.maxHealth;
        }
    }

    public void Heal(int amount)
    {
        playerHealthData.currentHealth += amount;

        if (playerHealthData.currentHealth > playerHealthData.maxHealth)
        {
            playerHealthData.currentHealth = playerHealthData.maxHealth;
        }

        Debug.Log("Salud del jugador: " + playerHealthData.currentHealth);
    }

    public void TakeDamage(int amount)
    {
        playerHealthData.currentHealth -= amount;

        if (playerHealthData.currentHealth < 0)
        {
            playerHealthData.currentHealth = 0;
        }

        Debug.Log("Salud del jugador: " + playerHealthData.currentHealth);
    }
}
