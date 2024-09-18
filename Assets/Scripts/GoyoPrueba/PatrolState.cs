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

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float visionRange;
    [SerializeField] private float rayHeight;
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
        if (waypoints.Length > 0)
        {
            enemyGoyo.transform.position = waypoints[currentWaypointIndex].position;
        }
    }

    public void UpdateEnemyState()
    {
        Debug.Log("Entro en patrol");

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
        Debug.Log("Slio de patrol");
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

        // Solo mover en el eje X
        float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, move_speed * Time.deltaTime);
        enemyGoyo.transform.position = new Vector2(newX, currentPosition.y);
    }

   
}
