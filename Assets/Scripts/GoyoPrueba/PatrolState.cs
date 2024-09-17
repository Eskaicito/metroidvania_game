using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IStateEnemy
{
    public EnemyGoyo enemyGoyo;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float move_speed = 2.0f;

    public PatrolState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
        waypoints = enemyGoyo.waypoints;
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

        if (Vector3.Distance(enemyGoyo.transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        if (Input.GetKeyDown(KeyCode.L)) // Condicion cambio a chase
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }

    }

    public void ExitEnemyState()
    {

    }

    private void MoveToWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 currentPosition = enemyGoyo.transform.position;

        // Solo mover en el eje X
        float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, move_speed * Time.deltaTime);
        enemyGoyo.transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
    }
}
