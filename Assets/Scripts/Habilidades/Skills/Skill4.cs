using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill4 Activated");
        
    }

    public void Use()
    {
        Debug.Log("Skill4 Used");
       
    }
}
