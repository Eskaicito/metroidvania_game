using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 2f;
    private float nextAttackTime;

    [SerializeField] private float patrolSpeed = 4f; // Velocidad de patrulla más rápida
    [SerializeField] private float chaseSpeed = 6f;
    [SerializeField] private float pushForce = 300f;

    [SerializeField] private float detectionRange = 2f;
    [SerializeField] private float losePlayerRange = 4f; // Distancia a la que deja de seguir al jugador
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private bool isPlayerInRange = false;
    private Transform player;
    private bool isChasingPlayer = false;
    private bool returningToPatrol = false;

    private float detectionCooldown = 1f; // Tiempo para decidir si deja de perseguir al jugador
    private float detectionCooldownTimer;


    [SerializeField] string id;
    public string Id => id;

    [SerializeField] private PotionFactory potionFactory;

    private Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    private Collider2D enemyCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Mejora suavidad en el movimiento
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasingPlayer = true;
            returningToPatrol = false;
        }
        else if (isChasingPlayer && distanceToPlayer > losePlayerRange)
        {
            detectionCooldownTimer += Time.deltaTime;
            if (detectionCooldownTimer >= detectionCooldown)
            {
                isChasingPlayer = false;
                returningToPatrol = true; // Iniciar retorno a patrullaje
                detectionCooldownTimer = 0f;
            }
        }

        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else if (returningToPatrol)
        {
            ReturnToPatrol();
        }
        else
        {
            Patrol();
        }

        if (isPlayerInRange)
        {
            if (Time.time >= nextAttackTime)
            {
               
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, chaseSpeed * Time.deltaTime));
        }
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetWaypoint.position, patrolSpeed * Time.deltaTime));

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void ReturnToPatrol()
    {
        // Continúa desde donde está el enemigo para volver a patrullar
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetWaypoint.position, patrolSpeed * Time.deltaTime));

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            returningToPatrol = false; // Ha regresado al patrullaje normal
        }
    }

    private void AttackPlayer()
    {
        if (player != null)
        {
            Player playerHealth = player.GetComponent<Player>();
            if (playerHealth != null)
            {
                
                playerHealth.TakeDamage((int)damage);
                PushPlayerBack(true); // Empujón más fuerte al atacar
            }
        }
    }

    private void PushPlayerBack(bool isAttacking = false)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 pushDirection = (player.position - transform.position).normalized;

            // Limitar la fuerza si el empuje es hacia arriba (evita que salga volando)
            if (pushDirection.y > 0)
            {
                pushDirection.y *= 0.3f; // Reduces la fuerza vertical
            }

            // Ajustar la fuerza de empuje horizontal según la dirección
            float clampedForce = Mathf.Clamp(pushForce, 0, 500f);
            playerRb.AddForce(pushDirection * clampedForce, ForceMode2D.Impulse);
            AudioManager.instance.PlaySound("hurt");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
        DropPotion();
    }

    private void DropPotion()
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
                    PushPlayerBack();
                }
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
