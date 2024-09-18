using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IStateEnemy
{
    private EnemyGoyo enemyGoyo;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float moveSpeed = 2.0f;

    [SerializeField] private float detectionRange = 5.0f; // Rango de detección del jugador
    private Transform playerTransform;

    public PatrolState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
        waypoints = enemyGoyo.Waypoints;
        playerTransform = enemyGoyo.PlayerTransform;
    }

    public void EnterEnemyState()
    {
        Debug.Log("Entrando a estado de PATRULLA");

        // Evitar mover al enemigo al primer waypoint al entrar de nuevo en patrullaje
        if (waypoints.Length > 0 && currentWaypointIndex == 0)
        {
            // Solo colocar al enemigo en el waypoint si es la primera vez
            enemyGoyo.transform.position = waypoints[currentWaypointIndex].position;
        }
    }

    public void UpdateEnemyState()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        // Mover hacia el siguiente waypoint
        MoveToWaypoint();

        // Si llegamos al waypoint actual, avanzamos al siguiente
        if (Vector2.Distance(enemyGoyo.transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Comprobar si el jugador está en rango
        CheckPlayerInRange();
    }

    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de PATRULLA");
    }

    private void CheckPlayerInRange()
    {
        // Si el jugador está dentro del rango de detección, cambiar al estado de persecución
        if (Vector2.Distance(enemyGoyo.transform.position, playerTransform.position) <= detectionRange)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }
    }

    private void MoveToWaypoint()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 currentPosition = enemyGoyo.transform.position;

        // Mover al enemigo hacia el siguiente waypoint
        enemyGoyo.transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}