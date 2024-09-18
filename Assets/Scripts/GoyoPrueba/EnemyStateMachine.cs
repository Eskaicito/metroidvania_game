using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IStateEnemy currentState;

    public IStateEnemy CurrentState => currentState;

    public ChaseState chaseState;
    public AttackState attackState;
    public PatrolState patrolState;
    public EnemyIdleState enemyIdleState;


    public EnemyStateMachine(EnemyGoyo enemyGoyo)
    {
        this.chaseState = new ChaseState(enemyGoyo);
        this.patrolState = new PatrolState(enemyGoyo);
        //this.attackState = new AttackState();
        //this.enemyIdleState = new EnemyIdleState();
    }

    public void Initialize(IStateEnemy enemyState)
    {
        currentState = enemyState;
        enemyState.EnterEnemyState();
    }

    public void TransitionTo(IStateEnemy enemyState)
    {
        currentState.ExitEnemyState();
        currentState = enemyState;
        currentState.EnterEnemyState();
    }

    public void UpdateState()
    {
        currentState.UpdateEnemyState();
    }
}
