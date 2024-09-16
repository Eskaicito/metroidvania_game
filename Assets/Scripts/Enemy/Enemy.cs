using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] float speed = 3f;
    public float Speed => speed; // Propiedad

    [SerializeField] string id;
    public string Id => id; // Propiedad

    private void Start()
    {
       
    }

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
