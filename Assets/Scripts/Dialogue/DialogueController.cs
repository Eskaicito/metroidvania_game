using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;    
    [SerializeField] GameObject dialogueUI;           
    [SerializeField] Button optionAButton;            
    [SerializeField] Button optionBButton;            

    private System.Action onOptionA;                  
    private System.Action onOptionB;                 

    private void Start()
    {
        dialogueUI.SetActive(false);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        optionAButton.onClick.AddListener(OptionAChosen);
        optionBButton.onClick.AddListener(OptionBChosen);
    }

    
    public void StartDialogueWithOptions(string dialogue, System.Action optionAAction, System.Action optionBAction)
    {
        dialogueUI.SetActive(true);
        onOptionA = optionAAction;
        onOptionB = optionBAction;
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        StartCoroutine(TypeDialogue(dialogue, true)); 
    }

    
    public void StartSimpleDialogue(string dialogue)
    {
        dialogueUI.SetActive(true);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        StartCoroutine(TypeDialogue(dialogue, false)); 
    }

    
    private IEnumerator TypeDialogue(string dialogue, bool hasOptions)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); 
        }

        if (hasOptions)
        {
            
            optionAButton.gameObject.SetActive(true);
            optionBButton.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(CloseAfterDelay(2f));
        }
    }

    
    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndDialogue();
    }

   
    private void OptionAChosen()
    {
        Debug.Log("Elegí la opción A");
        onOptionA?.Invoke();  
        EndDialogue();
    }

   
    private void OptionBChosen()
    {
        Debug.Log("Elegí la opción B");
        onOptionB?.Invoke();  
        EndDialogue();
    }

   
    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);
    }
}
