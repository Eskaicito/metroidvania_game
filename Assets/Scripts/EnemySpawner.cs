using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
  
    void Start()
    {
        enemyFactory.Create("Flyer");
        enemyFactory.Create("Patroller");
    }

  
}
