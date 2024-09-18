using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill1 Activated");
    }

    public void Use()
    {
        Debug.Log("Skill1 Used");
    }
}
