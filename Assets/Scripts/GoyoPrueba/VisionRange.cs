using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
    [SerializeField] private EnemyYT enemyYT;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyYT != null)
        {
            enemyYT.Pursuing = true;
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyYT != null)
        {
            enemyYT.Pursuing = false;
            enemyYT.attacking = false;
        }
    }
}
