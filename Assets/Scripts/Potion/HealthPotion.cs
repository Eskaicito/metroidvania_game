using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPotion : Potion, ICollectible
{
    public int healthAmount = 30;

    private Player playerHealth;

   private void Start()
    {
        playerHealth = FindAnyObjectByType<Player>();
    }
    public void Collect()
    {

        if (playerHealth != null)
        {

            playerHealth.Heal(healthAmount);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound("heal");

            Collect();
        }
    }
}

