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
    [SerializeField] private float pushForce = 300f;
    public float Speed => speed;

    [SerializeField] string id;
    public string Id => id;

    [SerializeField] private PotionFactory potionFactory;

    [SerializeField] private float detectionRange = 2f;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private bool isPlayerInRange = false;
    private Transform player;
    private bool isChasingPlayer = false;
    private Vector2 lastWaypointPosition;

    private Collider2D enemyCollider;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        lastWaypointPosition = waypoints[currentWaypointIndex].position; // Posición inicial del waypoint

        // Ignorar colisiones entre el enemigo y el jugador
        //Collider2D playerCollider = player.GetComponent<Collider2D>();
        //enemyCollider = GetComponent<Collider2D>();
        //if (playerCollider != null && enemyCollider != null)
        //{
        //    Physics2D.IgnoreCollision(playerCollider, enemyCollider);
        //}
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            
            isChasingPlayer = true;
        }
        else if (isChasingPlayer)
        {
     
            isChasingPlayer = false;
            TeleportToWaypoint();
        }

        if (isChasingPlayer)
        {
            ChasePlayer();
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
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
            lastWaypointPosition = waypoints[currentWaypointIndex].position; 
        }
    }

    private void TeleportToWaypoint()
    {
    
        transform.position = lastWaypointPosition;
        isChasingPlayer = false;
    }

    private void AttackPlayer()
    {
        if (player != null)
        {
            Player playerHealth = player.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
                PushPlayerBack(); 
            }
        }
    }

    private void PushPlayerBack()
    {
       
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
           
            Vector2 pushDirection = (player.position - transform.position).normalized;

         
            playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
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
                    PushPlayerBack();
                }

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
