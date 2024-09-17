using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYT : MonoBehaviour
{
    public int rutine;
    public float cronometer;

    public int direction;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool attacking;     // Solo para ataques cercanos
    public bool Pursuing;      // Para la persecución cuando el jugador está en rango de visión

    private float range_vision;
    private float range_attack;
    public GameObject range;
    public GameObject Hit;

    void Start()
    {
        target = GameObject.Find("PJ");
    }

    public void Behaviours()
    {
        if (Pursuing)  // Perseguir si el jugador está en rango de visión
        {
            ChasePlayer();

            // Si el jugador está lo suficientemente cerca, activar el ataque
            if (Mathf.Abs(transform.position.x - target.transform.position.x) <= range_attack)
            {
                attacking = true;
                RotateTowardsPlayer();
                // Aquí puedes activar una animación de ataque o lógica de combate
            }
            else
            {
                attacking = false;
            }
        }
        else  // Si no está persiguiendo, patrullar
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        cronometer += 1 * Time.deltaTime;
        if (cronometer >= 2)
        {
            rutine = Random.Range(0, 2);
            cronometer = 0;
        }

        switch (rutine)
        {
            case 0:
                // Idle
                break;

            case 1:
                direction = Random.Range(0, 2);
                rutine++;
                break;

            case 2:
                // Mover en la dirección seleccionada
                MoveInDirection();
                break;
        }
    }

    private void MoveInDirection()
    {
        switch (direction)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                break;

            case 1:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                break;
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            // Mover hacia el jugador
            transform.Translate(Vector3.right * speed_run * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.Translate(Vector3.right * speed_run * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void RotateTowardsPlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Final()
    {
        attacking = false;
        range.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        Behaviours();
    }
}

