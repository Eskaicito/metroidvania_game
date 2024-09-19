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

    private float detectionRange = 5.0f; 

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

        if (waypoints.Length > 0 && currentWaypointIndex == 0)
        {
            enemyGoyo.transform.position = waypoints[currentWaypointIndex].position;
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
        if (Vector2.Distance(enemyGoyo.transform.position, playerTransform.position) <= detectionRange)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }
    }

    private void MoveToWaypoint()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 currentPosition = enemyGoyo.transform.position;

        if (enemyGoyo.CompareTag("Ground"))
        {
            float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, moveSpeed * Time.deltaTime);
            enemyGoyo.transform.position = new Vector2(newX, currentPosition.y); // Mantener fija la Y
        }
        else if (enemyGoyo.CompareTag("Air"))
        {
            float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, moveSpeed * Time.deltaTime);
            float fixedY = currentPosition.y;
            enemyGoyo.transform.position = new Vector2(newX, fixedY);
        }

        
        
    }
}