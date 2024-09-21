using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState :  IStateEnemy
{
    private EnemyGoyo enemyGoyo;
    private float attackDuration = 1.5f;
    private float attackTimer;

    public AttackState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
    }

    public void EnterEnemyState()
    {
        Debug.Log("Entrando a estado ATTACK");
        attackTimer = attackDuration;
    }

    public void UpdateEnemyState()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            Debug.Log("Atacando al jugador");
            enemyGoyo.EnemyStateMachine.TransitionTo(enemyGoyo.EnemyStateMachine.chaseState);
        }
    }

    public void ExitEnemyState()
    {
        Debug.Log("Saliendo de attack");
    }
}
