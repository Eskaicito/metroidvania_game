using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWheel : MonoBehaviour
{

    public static SkillWheel Instance;
    public Canvas skillWheelCanvas;
    public Image wheelImage;
    public float wheelRadius = 200f; // Radio de la rueda de habilidades
    public float skillHighlightSize = 1.2f; // Tama�o de la habilidad resaltada
    public Dictionary<string, SkillBase> skills = new Dictionary<string, SkillBase>();
    private bool isWheelActive = false;
    private Image highlightedSkill;
    private SkillBase selectedSkill;
    private RectTransform wheelRectTransform;
    private Vector2 wheelCenter;
    private List<SkillWheelButton> skillButtons = new List<SkillWheelButton>();

    [System.Serializable]
    public class SkillWheelButton
    {
        public string skillName;
        public Image skillIcon;
        public SkillBase skillScript;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wheelRectTransform = wheelImage.rectTransform;
        wheelCenter = wheelRectTransform.rect.size / 2f; // Center the wheel center

        skillWheelCanvas.enabled = false;

        
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            isWheelActive = !isWheelActive;
            skillWheelCanvas.enabled = isWheelActive;

            // Pause or resume the game
            Time.timeScale = isWheelActive ? 0f : 1f;
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
                if (Input.GetMouseButtonUp(1)) // Right-click release
                {
                    SelectSkill(button.skillName);
                    isWheelActive = false;
                    skillWheelCanvas.enabled = false;
                    Time.timeScale = 1f; // Resume gameplay
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
            selectedSkill = skills[skillName];
            Debug.Log("Selected skill: " + skillName);
            // Set the selected skill as the active skill for the player
            selectedSkill.Activate();
        }
    }

    public void AddSkill(string skillName, SkillBase skillScript, Sprite skillSprite)
    {
        if (!skills.ContainsKey(skillName))
        {
            skills.Add(skillName, skillScript);

            // Create and position new skill icon
            GameObject skillObject = new GameObject(skillName);
            Image newSkillImage = skillObject.AddComponent<Image>();
            newSkillImage.sprite = skillSprite;
            newSkillImage.transform.SetParent(wheelImage.transform, false); // Set the parent without affecting the local scale
            newSkillImage.rectTransform.sizeDelta = new Vector2(50, 50); // Ajustar tama�o de �conos si es necesario
            newSkillImage.rectTransform.pivot = new Vector2(0.5f, 0.5f); // Center the pivot

            // Calculate position in a circular pattern
            float angleStep = 360f / Mathf.Max(1, skillButtons.Count); // Ensure at least one angle step
            float angle = skillButtons.Count * angleStep;
            float radian = angle * Mathf.Deg2Rad;
            newSkillImage.rectTransform.anchoredPosition = new Vector2(Mathf.Cos(radian) * wheelRadius, Mathf.Sin(radian) * wheelRadius);

            skillButtons.Add(new SkillWheelButton { skillName = skillName, skillIcon = newSkillImage, skillScript = skillScript });

            // Update all skill button positions after adding a new skill
            UpdateSkillButtonPositions();
        }
    }

    private void UpdateSkillButtonPositions()
    {
        int numSkills = skillButtons.Count;
        float angleStep = 360f / numSkills;

        for (int i = 0; i < numSkills; i++)
        {
            float angle = i * angleStep;
            float radian = angle * Mathf.Deg2Rad;
            Vector2 position = new Vector2(Mathf.Cos(radian) * wheelRadius, Mathf.Sin(radian) * wheelRadius);

            skillButtons[i].skillIcon.rectTransform.anchoredPosition = position;
        }
    }
}