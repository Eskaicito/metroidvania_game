using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    private Player player;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private bool isPlayerInRange = false;
    private float nextAttackTime;

    [SerializeField] float speed = 3f;
    public float Speed => speed; // Propiedad

    [SerializeField] string id;
    public string Id => id; // Propiedad

    [SerializeField] public GameObject healthPotion;


    private void Update()
    {
        if (isPlayerInRange && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Attempting to attack player.");

        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            Player playerHealth = player.GetComponent<Player>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
                Debug.Log("Enemy attacked player for " + damage + " damage. Player's current health: " + playerHealth.currentHealth);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Rango de detección
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rango de ataque
    }



}
