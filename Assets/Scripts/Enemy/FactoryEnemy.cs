using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    [SerializeField] List<Enemy> enemyList;
    private Dictionary<string , Enemy> idEnemies;

    private void Awake()
    {
        idEnemies = new Dictionary<string , Enemy>();

        foreach (Enemy enemy in enemyList)
        {
            idEnemies.Add(enemy.Id, enemy);
        }
    }

    public Enemy Create(string id)
    {
        if(idEnemies.TryGetValue(id, out Enemy enemy))
        {
            return Instantiate(enemy);
            
        }
        return null;

    }
}
