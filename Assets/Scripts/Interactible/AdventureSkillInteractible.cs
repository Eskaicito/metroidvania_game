using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureSkillInteractible : MonoBehaviour
{
    public string requiredSkill;  
    public Transform targetWaypoint;  
    public float grappleSpeed = 10f;  
    public float upwardImpulse = 5f;  
    private bool isGrappling = false;
    private Vector3 grappleTarget;
    private Rigidbody2D playerRb;
    [SerializeField] private GameObject indicator;
    [SerializeField] float floatHeight = 0.1f;
    [SerializeField] float floatSpeed = 0.5f;
  

    private void Start()
    {
        indicator.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 1.5f);
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        Vector3 newPosition = indicator.transform.position + new Vector3(0f, offsetY, 0f);

        indicator.transform.position = newPosition;
    }

   
    public void Interact()
    {
        
        if (CollectibleAdventureSkill.acquiredSkills.ContainsKey(requiredSkill) && CollectibleAdventureSkill.acquiredSkills[requiredSkill])
        {
            Debug.Log($"Interactuando con el objeto usando la habilidad {requiredSkill}.");
            UseSkill();
        }
        else
        {
            Debug.Log($"No tienes la habilidad {requiredSkill} para interactuar.");
        }
    }

    private void UseSkill()
    {
      
        if (!isGrappling)
        {
            grappleTarget = targetWaypoint.position;
            isGrappling = true;
            playerRb.gravityScale = 0; 
            StartCoroutine(GrappleMovement());
        }
    }


    private IEnumerator GrappleMovement()
    {
        while (isGrappling)
        {
       
            playerRb.transform.position = Vector3.MoveTowards(playerRb.transform.position, grappleTarget, grappleSpeed * Time.deltaTime);

            if (Vector3.Distance(playerRb.transform.position, grappleTarget) < 0.1f)
            {
              
                isGrappling = false;

               
                playerRb.gravityScale = 1; 
                playerRb.AddForce(Vector2.up * upwardImpulse, ForceMode2D.Impulse);

                Debug.Log("Jugador ha llegado al waypoint y ha recibido un impulso hacia arriba.");
            }

            yield return null;  
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
}
