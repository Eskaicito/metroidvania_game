using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IStateEnemy
{
    private EnemyGoyo enemyGoyo;
    private FlyingEnemy flyingEnemy;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float moveSpeed = 2.0f;
    private float detectionGroundRange = 5.0f;
    private float detectionAirRange = 1.0f;
    private float detectionRange = 1.0f;
    

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

        // Asegurarse de que el enemigo no se teletransporte al waypoint al volver al patrullaje
        if (waypoints.Length > 0)
        {
            // Si es la primera vez que entra en patrullaje, establecer la posición en el waypoint actual
            if (currentWaypointIndex == 0)
            {
                enemyGoyo.transform.position = waypoints[currentWaypointIndex].position;
            }
        }
    }

    public void UpdateEnemyState()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        
        MoveToWaypoint();

        
        if (Vector2.Distance(enemyGoyo.transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        
        CheckPlayerInRange();
    }

    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de PATRULLA");
    }

    private void CheckPlayerInRange()
    {
       
        if (Vector2.Distance(enemyGoyo.transform.position, playerTransform.position) <= detectionGroundRange)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }
    }

    private void MoveToWaypoint()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 currentPosition = enemyGoyo.transform.position;

        
        float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, moveSpeed * Time.deltaTime);
        enemyGoyo.transform.position = new Vector2(newX, currentPosition.y);
    }
}