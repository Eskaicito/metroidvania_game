using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 2f;
    private float nextAttackTime;

    [SerializeField] float speed = 3f;
    public float Speed => speed; // Propiedad

    [SerializeField] string id;
    public string Id => id; // Propiedad

    [SerializeField] private PotionFactory potionFactory; // Referencia al factory

    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void AttackPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Player playerHealth = player.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
                Debug.Log("Enemy attacked player for " + damage + " damage. Player's current health: " + playerHealth.playerHealthData.currentHealth);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entro en rango");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player salio del rango");
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

        DropPotion(); 
    }

    
    public void DropPotion()
    {
        if (potionFactory != null)
        {
            potionFactory.DropPotion(transform.position);
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
                    Debug.Log("Enemy attacked player for " + damage + " damage. Player's current health: " + player.playerHealthData.currentHealth);
                }

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
