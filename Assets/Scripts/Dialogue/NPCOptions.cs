using UnityEngine;

public class NPCOptions : MonoBehaviour, IInteractible
{
    [SerializeField] DialogueController dialogueController;
    [SerializeField] KarmaSystem karmaSystem;  
    [SerializeField] string npcDialogue;
    [SerializeField] GameObject interactIcon;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactIcon.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactIcon.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void Start()
    {
        interactIcon.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void Interact()
    {
        
        dialogueController.StartDialogueWithOptions(
            npcDialogue,
            () => {
                Debug.Log("REDENCION");
                karmaSystem.AddRedencionPoints(20);  
                Destroy(gameObject);
            }, 
            () => {
                Debug.Log("VENGANZA");
                karmaSystem.AddVenganzaPoints(20);  
                Destroy(gameObject);
            }   
        );
    }
}
