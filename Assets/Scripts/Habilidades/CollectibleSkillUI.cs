using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleSkillUI : MonoBehaviour
{
    public GameObject skillPanel;        
    public Image skillIconUI;        
    public TextMeshProUGUI skillDescriptionUI; 
    public Button closeButton;        

    private float animationDuration = 0.5f; 
    private Vector3 initialScale;            
    private Vector3 targetScale = Vector3.one;  

    private void Start()
    {
      
        initialScale = new Vector3(0, 0, 1);
        skillPanel.transform.localScale = initialScale;
        skillPanel.SetActive(false); 

   
        skillIconUI.gameObject.SetActive(false);
        skillDescriptionUI.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);


        closeButton.onClick.AddListener(HideSkillPanel);
    }

    public void ShowSkillPanel(Sprite skillIcon, string description)
    {
    
        skillIconUI.sprite = skillIcon;
        skillDescriptionUI.text = description;

        skillPanel.SetActive(true);
        skillIconUI.gameObject.SetActive(true);
        skillDescriptionUI.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);

   
        StopAllCoroutines();
        StartCoroutine(AnimatePanel(true));
    }

    public void HideSkillPanel()
    {
       
        StopAllCoroutines();
        StartCoroutine(AnimatePanel(false));
    }

    private IEnumerator AnimatePanel(bool appearing)
    {
        float timeElapsed = 0;
        Vector3 startScale = skillPanel.transform.localScale;
        Vector3 endScale = appearing ? targetScale : initialScale;

 
        Color startColor = skillIconUI.color;
        Color endColor = appearing ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        Color startTextColor = skillDescriptionUI.color;
        Color endTextColor = appearing ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);

        while (timeElapsed < animationDuration)
        {
    
            skillPanel.transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / animationDuration);

          
            skillIconUI.color = Color.Lerp(startColor, endColor, timeElapsed / animationDuration);
            skillDescriptionUI.color = Color.Lerp(startTextColor, endTextColor, timeElapsed / animationDuration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }


        skillPanel.transform.localScale = endScale;
        skillIconUI.color = endColor;
        skillDescriptionUI.color = endTextColor;


        if (!appearing)
        {
            skillIconUI.gameObject.SetActive(false);
            skillDescriptionUI.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            skillPanel.SetActive(false);
        }
    }
}
