using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;


public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    private Dictionary<string, Enemy> idEnemies;

    private void Awake()
    {
        idEnemies = new Dictionary<string, Enemy>();

        foreach (Enemy enemy in enemies)
        {
            idEnemies.Add(enemy.Id, enemy);
        }
    }

    public Enemy Create(string id, Vector3 position )
    {
        if (idEnemies.TryGetValue(id, out Enemy enemy))
        {
            Enemy instantiatedEnemy = Instantiate(enemy);
            
            return instantiatedEnemy;
        }
        return null;
    }
}
