using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWheel : MonoBehaviour
{
    public Canvas skillWheelCanvas;
    public Image wheelImage;
    public float wheelRadius = 200f;
    public float skillHighlightSize = 1.2f;
    public Image activeSkillIconUI; 

    private Dictionary<string, ISkill> skills = new Dictionary<string, ISkill>();
    private bool isWheelActive = false;
    private Image highlightedSkill;
    private ISkill activeSkill;
    public ISkill skillActive => activeSkill;
    private RectTransform wheelRectTransform;
    private Vector2 wheelCenter;
    private List<SkillWheelButton> skillButtons = new List<SkillWheelButton>();


    [System.Serializable]
    public class SkillWheelButton
    {
        public string skillName;
        public Image skillIcon;
        public ISkill skillScript;
    }


    private void Start()
    {
        wheelRectTransform = wheelImage.rectTransform;
        wheelCenter = wheelRectTransform.rect.size / 2f;
        skillWheelCanvas.enabled = false;
        if (activeSkillIconUI != null)
        {
            activeSkillIconUI.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            isWheelActive = !isWheelActive;
            skillWheelCanvas.enabled = isWheelActive;
            Time.timeScale = isWheelActive ? 0f : 1f;
            if (!isWheelActive && activeSkillIconUI != null)
            {
                activeSkillIconUI.gameObject.SetActive(true);
            }
        }

        if (isWheelActive)
        {
            HandleSkillWheel();
        }
    }

    private void HandleSkillWheel()
    {

        Vector2 mousePosition = Input.mousePosition;
        Vector2 wheelPosition = wheelRectTransform.position;
        Vector2 direction = mousePosition - wheelPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        foreach (var button in skillButtons)
        {
            Vector2 buttonPosition = button.skillIcon.rectTransform.anchoredPosition;
            Vector2 buttonDirection = buttonPosition - wheelCenter;
            float buttonAngle = Mathf.Atan2(buttonDirection.y, buttonDirection.x) * Mathf.Rad2Deg;
            if (buttonAngle < 0) buttonAngle += 360;

            float angleDifference = Mathf.Abs(angle - buttonAngle);
            angleDifference = Mathf.Min(angleDifference, 360f - angleDifference);

          

            if (angleDifference < 360f / skillButtons.Count / 2)
            {
                HighlightSkill(button.skillIcon);
                if (Input.GetMouseButtonUp(1))
                {
                    SelectSkill(button.skillName);
                    isWheelActive = false;
                    skillWheelCanvas.enabled = false;
                    Time.timeScale = 1f;
                }
            }
            else
            {
                RemoveHighlight(button.skillIcon);
            }
        }
    }

    private void HighlightSkill(Image skillImage)
    {
        if (highlightedSkill != null && highlightedSkill != skillImage)
        {
            RemoveHighlight(highlightedSkill);
        }

        skillImage.transform.localScale = Vector3.one * skillHighlightSize;
        highlightedSkill = skillImage;
    }

    private void RemoveHighlight(Image skillImage)
    {
        skillImage.transform.localScale = Vector3.one;
    }

    private void SelectSkill(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            AudioManager.instance.PlaySound("collect");
            activeSkill = skills[skillName];
            Debug.Log("Selected skill: " + skillName);
            activeSkill.Activate();

           
            activeSkillIconUI.sprite = skillButtons.Find(button => button.skillName == skillName).skillIcon.sprite;

        }
    }

    public void AddSkill(string skillName, ISkill skillScript, Sprite skillSprite)
    {
        if (!skills.ContainsKey(skillName))
        {
            skills.Add(skillName, skillScript);

            GameObject skillObject = new GameObject(skillName);
            Image newSkillImage = skillObject.AddComponent<Image>();
            newSkillImage.sprite = skillSprite;
            newSkillImage.transform.SetParent(wheelImage.transform, false);
            newSkillImage.rectTransform.sizeDelta = new Vector2(50, 50);
            newSkillImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);

            skillButtons.Add(new SkillWheelButton { skillName = skillName, skillIcon = newSkillImage, skillScript = skillScript });

            // Actualiza posiciones después de añadir la habilidad
            UpdateSkillButtonPositions();
        }
    }

    private void UpdateSkillButtonPositions()
    {
        int numSkills = skillButtons.Count;
        if (numSkills == 0) return;

        float angleStep = 360f / numSkills;

        for (int i = 0; i < numSkills; i++)
        {
            float angle = i * angleStep;
            float radian = angle * Mathf.Deg2Rad;
            Vector2 position = new Vector2(Mathf.Cos(radian) * wheelRadius, Mathf.Sin(radian) * wheelRadius);

            skillButtons[i].skillIcon.rectTransform.anchoredPosition = position;
        }
    }

    public ISkill GetActiveSkill()
    {
        return activeSkill;
    }
}
