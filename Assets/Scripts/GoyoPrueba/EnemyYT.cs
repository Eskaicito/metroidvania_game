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
    [SerializeField] public bool Attacking;

    [SerializeField] private float vision_range;
    [SerializeField] private float attack_range;
    [SerializeField] private GameObject Hit;
    [SerializeField] private GameObject Range;

    private void Start()
    {
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > vision_range && !Attacking)
        {
            cronometer += 1 * Time.deltaTime;
            if (cronometer >= 4)
            {
                rutine = Random.Range(0, 2);
                cronometer = 0;
            }
        
            switch (rutine)
            {
                case 0:
                    direction = Random.Range(0, 2);
                    rutine++;
                break;

                case 1:
                    switch (direction)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                        break;

                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                        break;

                    }
                break;



            }

        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > attack_range && !Attacking)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                }
                else
                {

                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);

                }

            }
            else
            {
                if (!Attacking)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
            }
            
        }

    }

    public void Final()
    {
        Attacking = false;
        Range.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent <BoxCollider2D>().enabled = false;
    }
}
