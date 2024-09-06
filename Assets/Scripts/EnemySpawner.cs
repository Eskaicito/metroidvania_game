using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
  
    void Start()
    {
        Vector3 flyerPosition = new Vector3(0, 1, 0);
        Vector3 patrollerPosition = new Vector3(5, 1, 0);
        
         enemyFactory.Create("Flyer", flyerPosition);
         enemyFactory.Create("Patroller", patrollerPosition );
    }

  
}
