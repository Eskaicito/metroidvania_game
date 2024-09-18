using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ChaseState : IStateEnemy
{

    private EnemyGoyo enemyGoyo;
    [SerializeField] private float chaseSpeed = 5.0F;
    [SerializeField] private Transform playerTransform;
    
    


    public ChaseState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
        playerTransform = enemyGoyo.PlayerTransform;
    }

    public void EnterEnemyState()
    {
        Debug.Log("Entrando a estado CHASE");
    }

    public void UpdateEnemyState()
    {
        if (enemyGoyo.PlayerTransform != null)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.patrolState);
            return;
        }

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (enemyGoyo.PlayerTransform == null)
            return;

        Vector2 targetPosition = enemyGoyo.PlayerTransform.position;
        Vector2 currentPosition = enemyGoyo.transform.position;

        // Mover hacia el jugador
        Vector2 direction = (targetPosition - currentPosition).normalized;
        enemyGoyo.transform.position = Vector2.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
    }


    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de CHASE");
    }

   

    

}
