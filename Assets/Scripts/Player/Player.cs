using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;

        
        healthBar.HealthMax = maxHealth;
        healthBar.ActualHealth = currentHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        
        healthBar.ActualHealth = currentHealth;

        Debug.Log("Salud del jugador: " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        
        healthBar.ActualHealth = currentHealth;

        Debug.Log("Salud del jugador: " + currentHealth);
    }
}
