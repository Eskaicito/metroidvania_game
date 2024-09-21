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

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform hookOrigin;      

    private void Start()
    {
        indicator.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2f);
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

       
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2;  
        lineRenderer.enabled = false;    
        lineRenderer.startWidth = 0.1f;  
        lineRenderer.endWidth = 0.1f;    
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); 
        lineRenderer.startColor = Color.white; 
        lineRenderer.endColor = Color.white;   
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
            AudioManager.instance.PlaySound("hook");
            grappleTarget = targetWaypoint.position;
            isGrappling = true;
            playerRb.gravityScale = 0;
            lineRenderer.enabled = true; 
            StartCoroutine(GrappleMovement());
        }
    }

    private IEnumerator GrappleMovement()
    {
        while (isGrappling)
        {
            
            lineRenderer.SetPosition(0, hookOrigin.position); 
            lineRenderer.SetPosition(1, playerRb.transform.position); 

           
            playerRb.transform.position = Vector3.MoveTowards(playerRb.transform.position, grappleTarget, grappleSpeed * Time.deltaTime);

            if (Vector3.Distance(playerRb.transform.position, grappleTarget) < 0.1f)
            {
                isGrappling = false;
                lineRenderer.enabled = false; 
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
