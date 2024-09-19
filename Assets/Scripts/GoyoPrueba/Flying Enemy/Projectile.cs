using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Damage
            Destroy(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Update()
    {
        Destroy(gameObject, 1.5f);
    }
}
