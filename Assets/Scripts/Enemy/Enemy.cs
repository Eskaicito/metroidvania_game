using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 4f;
    private float nextAttackTime;

    [SerializeField] float speed = 3f;
    public float Speed => speed; // Propiedad

    [SerializeField] string id;
    public string Id => id; // Propiedad

    [SerializeField] public GameObject healthPotion;

    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private bool isPlayerInRange = false;

    private void Update()
    {
        if(isPlayerInRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void AttackPlayer()
    {
        GameObject player = GetComponent<GameObject>();
        if(player != null)
        {

        }
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

        DropHealthPotion();
    }

    public void DropHealthPotion()
    {
        if(healthPotion != null)
        {
            Instantiate(healthPotion, transform.position, Quaternion.identity);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                Player player = collision.gameObject.GetComponent<Player>();

                if (player != null)
                {
                    player.TakeDamage((int)damage);
                    Debug.Log("Enemy attacked player for " + damage + "damage. Players current health: " + player.currentHealth);
                }

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }



}
