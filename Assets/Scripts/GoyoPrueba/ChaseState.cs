using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IStateEnemy
{

    private EnemyGoyo enemyGoyo;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float visionRange;
    [SerializeField] private float rayHeight;
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

    }

    public void ExitEnemyState()
    {

    }
}
