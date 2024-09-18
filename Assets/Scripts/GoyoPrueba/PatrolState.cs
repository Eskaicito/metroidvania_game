using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IStateEnemy
{
    public EnemyGoyo enemyGoyo;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float move_speed = 2.0f;

    [SerializeField] private Transform playerTransform;
    private bool isPlayerInRange;

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

        MoveToWaypoint();

        if (Vector2.Distance(enemyGoyo.transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        if (isPlayerInRange)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }
    }

    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de PATRULLA");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == playerTransform)
        {
            isPlayerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == playerTransform)
        {
            isPlayerInRange = false;
        }
    }

    private void MoveToWaypoint()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 currentPosition = enemyGoyo.transform.position;

        // Solo mover en el eje X para mantener el patrullaje sin teletransportar
        float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, move_speed * Time.deltaTime);
        enemyGoyo.transform.position = new Vector2(newX, currentPosition.y);
    }
}