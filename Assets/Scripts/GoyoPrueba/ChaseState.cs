using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ChaseState : IStateEnemy
{

    private EnemyGoyo enemyGoyo;
    [SerializeField] private float chaseSpeed = 5.0F;
    [SerializeField] private float chaseDuration = 5.0f;
    [SerializeField] private float chaseTimer = 0f;
    [SerializeField] private Transform playerTransform;
    
    public ChaseState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
        playerTransform = enemyGoyo.PlayerTransform;
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
         chaseTimer-= Time.deltaTime;

        if(chaseTimer <= 0)
        {
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.patrolState);
        }
        
    }

    private void MoveTowardsPlayer()
    {
        Vector2 playerPosition = enemyGoyo.PlayerTransform.position;
        Vector2 currentPosition = enemyGoyo.transform.position;
        enemyGoyo.transform.position = Vector2.MoveTowards(currentPosition, playerPosition, chaseSpeed * Time.deltaTime);
    }


    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de CHASE");
    }

   

    

}
