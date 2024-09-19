using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private FactoryEnemy factoryEnemy;

    void Start()
    {
        factoryEnemy.Create("Flying");
        factoryEnemy.Create("Patrolling");

    }
}
