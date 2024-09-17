using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    [SerializeField] List<Enemy> enemyList;
    private Dictionary<string , Enemy> idEnemies;

    //[SerializeField] private GameObject healthPotion;


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
            Enemy newEnemy = Instantiate(enemy);
            
            //newEnemy.healthPotion = healthPotion;

            return newEnemy;
        }
        return null;

    }
}
