using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private float damage = 20f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private bool isPlayerInRange = false;
    private float nextAttackTime;

    [SerializeField] float speed = 3f;
    public float Speed => speed; // Propiedad

    [SerializeField] string id;
    public string Id => id; // Propiedad

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
                Debug.Log("Boss attacked player for " + damage + " damage. Player's current health: " + playerHealth.playerHealthData.currentHealth);
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
        Debug.Log("Boss took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss died!");

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) <= attackRange)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Time.time >= nextAttackTime)
                {
                    Player player = collision.gameObject.GetComponent<Player>();

                    if (player != null)
                    {
                        player.TakeDamage((int)damage);
                        Debug.Log("Boss attacked player for " + damage + " damage. Player's current health: " + player.playerHealthData.currentHealth);
                    }

                    nextAttackTime = Time.time + attackCooldown;
                }
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
