using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public EnemyStateMachine EnemyStateMachine => enemyStateMachine;

    [SerializeField] private Transform[] waypoints;
    public Transform[] Waypoints => waypoints;

    [SerializeField] private Transform playerTransform;
    public Transform PlayerTransform => playerTransform;

    private void Awake()
    {
        //enemyStateMachine = new EnemyStateMachine(this);
        enemyStateMachine.Initialize(enemyStateMachine.patrolState);
    }

    private void Update()
    {
        enemyStateMachine.UpdateState();
    }

}
