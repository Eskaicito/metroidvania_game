using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] bool isPlayerInRange;

    [SerializeField] public Transform player;
    public Transform Player => player; // Propiedad


    [SerializeField] float speed = 3f;
    public float Speed => speed; // Propiedad

    [SerializeField] string id;
    public string Id => id; // Propiedad

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public virtual void DetectPlayer()
    {
        // Dirección hacia el jugador
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Crear un raycast desde la posición del enemigo en la dirección del jugador
        RaycastHit hit;

        // Si el rayo impacta algo dentro del rango de detección
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            // Si el rayo golpea al jugador
            if (hit.collider.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
            {
                isPlayerInRange = true;
            }
            else
            {
                isPlayerInRange = false;
            }
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    public void Update()
    {
        DetectPlayer();
        if (isPlayerInRange)
        {
            Move();
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
    }

    public virtual void Move()
    {

    }

}
