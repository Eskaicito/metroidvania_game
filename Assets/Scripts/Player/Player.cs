using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal (int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Salud del jugador: " + currentHealth);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        //healthBar.SetHealth(currentHealth);

        Debug.Log("Salud del jugador: " + currentHealth);
    }
}
