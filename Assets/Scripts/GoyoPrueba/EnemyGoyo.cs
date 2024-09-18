using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoyo : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public EnemyStateMachine EnemyStateMachine => enemyStateMachine;

    [SerializeField] private Transform[] waypoints;
    public Transform[] Waypoints => waypoints;

    [SerializeField] private Transform playerTransform;
    public Transform PlayerTransform => playerTransform;

     private float chaseDuration = 5.0f;
     private float chaseTimer = 0f;

    public bool isPlayerInRange;

    

    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine(this);
        enemyStateMachine.Initialize(enemyStateMachine.patrolState);
    }

    private void Update()
    {
        Debug.Log("Cambiando estado");
        enemyStateMachine.UpdateState();

        if (isPlayerInRange)
        {
            enemyStateMachine.TransitionTo(enemyStateMachine.chaseState);
        }
        
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
}
