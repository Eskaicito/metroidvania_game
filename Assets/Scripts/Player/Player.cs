using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    // Referencia al ScriptableObject
    public PlayerHealthData playerHealthData;

    private void Start()
    {
        // Si es la primera vez que se carga el jugador, aseguramos que su vida y energ�a actual es igual a la m�xima
        if (playerHealthData.currentHealth <= 0)
        {
            playerHealthData.currentHealth = playerHealthData.maxHealth;
        }

        if (playerHealthData.currentEnergy <= 0)
        {
            playerHealthData.currentEnergy = playerHealthData.maxEnergy;
        }
    }

    private void Update()
    {
        // Detectar si se presiona la tecla J para perder energ�a
        if (Input.GetKeyDown(KeyCode.J))
        {
            UseEnergy(10); // Resta 10 puntos de energ�a al presionar J
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

    // M�todos para manipular la energ�a
    public void UseEnergy(int amount)
    {
        playerHealthData.currentEnergy -= amount;

        if (playerHealthData.currentEnergy < 0)
        {
            playerHealthData.currentEnergy = 0;
        }

        Debug.Log("Energ�a del jugador: " + playerHealthData.currentEnergy);
    }

    public void RestoreEnergy(int amount)
    {
        playerHealthData.currentEnergy += amount;

        if (playerHealthData.currentEnergy > playerHealthData.maxEnergy)
        {
            playerHealthData.currentEnergy = playerHealthData.maxEnergy;
        }

        Debug.Log("Energ�a del jugador: " + playerHealthData.currentEnergy);
    }
}
