using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ICollectible collectible = GetComponent<ICollectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
        }
    }
}
