using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;

    [SerializeField] string id;

    public string Id => id;

    public void TakeDamage(float damage)
    {
   
        health -= damage;

        
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);

       
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
   
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }
}
