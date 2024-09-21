using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAir : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform[] waypoints; 
    private int currentWaypointIndex = 0;

    [SerializeField] private PotionFactory potionFactory;

    [SerializeField] private float detectionRange = 5f; 
    private Transform player;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float projectileSpeed = 5f; 
    [SerializeField] private float fireCooldown = 2f; 
    private float nextFireTime;

    [SerializeField] private float health = 100f; 
    [SerializeField] private GameObject deathEffect; 

    private void Start()
    {
        // Inicializamos el jugador
        player = GameObject.FindWithTag("Player").transform;

        if (waypoints.Length < 2)
        {
            Debug.LogError("Necesitas asignar dos waypoints para el movimiento del enemigo.");
        }
    }

    private void Update()
    {
        MoveBetweenWaypoints(); 

        DetectAndShootPlayer(); 
    }

    private void MoveBetweenWaypoints()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        }
    }

    private void DetectAndShootPlayer()
    {
    
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

       
        if (distanceToPlayer <= detectionRange && Time.time >= nextFireTime)
        {
            ShootProjectile(); 
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
           
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

          
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

   
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    { 
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        DropPotion();
        Destroy(gameObject);
    }
    public void DropPotion()
    {
        if (potionFactory != null)
        {
            potionFactory.DropPotion(transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
