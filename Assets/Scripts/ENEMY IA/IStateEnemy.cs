using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateEnemy
{
    void EnterEnemyState();
    void ExitEnemyState();
    void UpdateEnemyState();
}
