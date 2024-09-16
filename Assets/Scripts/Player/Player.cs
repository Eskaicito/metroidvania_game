using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    private void Start()
    {
        currentHealth = 50;
    }

    public void Heal (int amount)
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Salud del jugador: " + currentHealth);
    }
}
