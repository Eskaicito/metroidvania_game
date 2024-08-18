using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        float horizontalInputX = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInputX, 0, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
