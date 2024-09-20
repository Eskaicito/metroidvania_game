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

    [SerializeField] private float health;

    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine(this);
        enemyStateMachine.Initialize(enemyStateMachine.patrolState);
    }

    private void Update()
    {
        enemyStateMachine.UpdateState();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        Destroy(gameObject);

    }

    
}