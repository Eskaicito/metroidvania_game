using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IStateEnemy
{
    public EnemyGoyo enemyGoyo;
    private Vector2 patrolAreaCenter;
    private Vector2 currentTargetPosition;
    private float move_speed = 2.0f;
    private float patrolRange = 5.0f; // Rango de patrullaje en unidades

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float visionRange;
    [SerializeField] private Transform playerTransform;

    private bool isPlayerInRange;
    private float waypointTolerance = 0.1f;

    public PatrolState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
        patrolAreaCenter = enemyGoyo.transform.position;
        SetRandomTargetPosition();
    }

    public void EnterEnemyState()
    {
        Debug.Log("Entrando en estado de patrullaje");
    }

    public void UpdateEnemyState()
    {
        Debug.Log("Actualizando estado de patrullaje");

        MoveToTargetPosition();

        // Si está cerca del objetivo, seleccionar un nuevo objetivo aleatorio
        if (Vector2.Distance(enemyGoyo.transform.position, currentTargetPosition) < waypointTolerance)
        {
            SetRandomTargetPosition();
        }

        // Verificar si el jugador está en el rango
        if (isPlayerInRange)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }
    }

    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de estado de patrullaje");
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

    private void SetRandomTargetPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRange;
        currentTargetPosition = (Vector2)patrolAreaCenter + randomOffset;
    }

    private void MoveToTargetPosition()
    {
        Vector2 currentPosition = enemyGoyo.transform.position;
        enemyGoyo.transform.position = Vector2.MoveTowards(currentPosition, currentTargetPosition, move_speed * Time.deltaTime);
    }
}
