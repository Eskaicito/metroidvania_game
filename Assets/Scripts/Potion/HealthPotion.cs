using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healthAmount = 5;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerHealth = collision.GetComponent<Player>(); 
            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
