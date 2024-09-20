using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ChaseState : IStateEnemy
{
     private EnemyGoyo enemyGoyo;
     private float chaseSpeed = 4.0F;
     private float chaseDuration = 2.0f;
     private float chaseTimer = 0f;

    private Transform attackColliderTransform;
    [SerializeField] private Transform playerTransform;

    public ChaseState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
        playerTransform = enemyGoyo.PlayerTransform;
        attackColliderTransform = enemyGoyo.transform.Find("AttackCollider");
    }

    public void EnterEnemyState()
    {
        Debug.Log("Entrando a estado CHASE");
        chaseTimer = chaseDuration;
    }

    public void UpdateEnemyState()
    {
        if (enemyGoyo.PlayerTransform != null)
        {
            MoveTowardsPlayer();
        }

        chaseTimer -= Time.deltaTime;

        CheckAttackCollider();

        if (chaseTimer <= 0)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.patrolState);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 playerPosition = enemyGoyo.PlayerTransform.position;
        Vector2 currentPosition = enemyGoyo.transform.position;

        // Mantener la posición Y fija
        float newX = Mathf.MoveTowards(currentPosition.x, playerPosition.x, chaseSpeed * Time.deltaTime);
        enemyGoyo.transform.position = new Vector2(newX, currentPosition.y); // La Y se mantiene fija
    }

    private void CheckAttackCollider()
    {
        float distanceToPlayer = Vector2.Distance(attackColliderTransform.position, playerTransform.position);

        // Asumiendo que quieres que cuando estén muy cerca cambie al estado de ataque
        if (distanceToPlayer < 0.5f) // Ajusta el valor según la cercanía que quieras
        {
            Debug.Log("Entrando en rango de ataque");
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.attackState);
        }
    }

    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de CHASE");
    }
}