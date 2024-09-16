using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public override void Move()
    {
        // Seguir al jugador en los ejes X, Y y Z (volador)
        Vector3 direction = (Player.position - transform.position).normalized;

        transform.position += direction * Speed * Time.deltaTime;

        // Rotar para mirar hacia el jugador si es necesario
        transform.LookAt(Player);
    }
}
