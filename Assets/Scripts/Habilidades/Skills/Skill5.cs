using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill5 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill5 Activated");
       
    }

    public void Use()
    {
        Debug.Log("Skill5 Used");
        
    }
}
