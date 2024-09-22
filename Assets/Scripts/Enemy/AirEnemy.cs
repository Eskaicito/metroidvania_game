using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Velocidad de movimiento entre waypoints
    [SerializeField] private Transform[] waypoints; // Dos waypoints para el movimiento
    private int currentWaypointIndex = 0;

    [SerializeField] private float detectionRange = 5f; // Rango de detección del jugador
    private Transform player;

    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private Transform firePoint; // Punto de donde salen los proyectiles
    [SerializeField] private float projectileSpeed = 5f; // Velocidad del proyectil
    [SerializeField] private float fireCooldown = 2f; // Tiempo de espera entre disparos
    private float nextFireTime;

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
        MoveBetweenWaypoints(); // Mover el enemigo entre waypoints

        DetectAndShootPlayer(); // Detectar al jugador y disparar si está en rango
    }

    private void MoveBetweenWaypoints()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Si llegamos al waypoint, avanzamos al siguiente
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Alternar entre los dos waypoints
        }
    }

    private void DetectAndShootPlayer()
    {
        // Calcula la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange && Time.time >= nextFireTime)
        {
            ShootProjectile(); // Disparar un proyectil
            nextFireTime = Time.time + fireCooldown; // Reiniciar el tiempo de espera para el próximo disparo
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instanciar el proyectil en el punto de disparo
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Aplicar fuerza para mover el proyectil hacia el jugador
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un círculo en el editor para visualizar el rango de detección
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
