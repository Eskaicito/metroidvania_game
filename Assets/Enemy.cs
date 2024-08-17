using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float damage)
    {
        // Reducir la salud del enemigo
        health -= damage;

        // Imprimir el daño recibido en la consola
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);

        // Comprobar si el enemigo ha muerto
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Mensaje en consola para indicar la muerte del enemigo
        Debug.Log("Enemy died!");

        // Desactivar o destruir el objeto enemigo
        // Aquí puedes poner animaciones de muerte, efectos, etc.
        Destroy(gameObject);
    }
}
