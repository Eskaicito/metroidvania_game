using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public override void Move()
    {
        // Seguir al jugador solo en el plano XZ (terrestre)
        Vector3 direction = (Player.position - transform.position).normalized;
        direction.y = 0; // Aseguramos que no suba o baje, solo en XZ

        transform.position += direction * Speed * Time.deltaTime;

        // Opcional: Rotar para que mire hacia el jugador
        transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));
    }
}
