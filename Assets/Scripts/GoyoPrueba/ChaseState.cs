using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IStateEnemy
{

    public EnemyGoyo enemyGoyo;

    public ChaseState(EnemyGoyo enemyGoyo)
    {
        this.enemyGoyo = enemyGoyo;
    }

    public void EnterEnemyState()
    {

    }

    public void UpdateEnemyState()
    {

    }

    public void ExitEnemyState()
    {

    }
}
