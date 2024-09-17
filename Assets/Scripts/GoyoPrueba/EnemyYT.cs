using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYT : MonoBehaviour
{
    [SerializeField] private int rutine;
    [SerializeField] private float cronometer;
    [SerializeField] private int direction;
    [SerializeField] private float speed_walk;
    [SerializeField] private float speed_run;
    [SerializeField] private GameObject target;
    [SerializeField] private bool isAttacking;

    private void Start()
    {
        target = GameObject.Find("Player");
    }

    public void Behaviour()
    {

    }
}
