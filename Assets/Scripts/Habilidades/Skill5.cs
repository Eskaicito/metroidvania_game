using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill5 : MonoBehaviour, ISkill
{
    public void Activate()
    {
        Debug.Log("Skill5 Activated");
        // Aquí podrías realizar configuraciones adicionales o mostrar un ícono
    }

    public void Use()
    {
        Debug.Log("Skill5 Used");
        // Aquí implementas la acción que realiza la habilidad cuando se usa
    }
}
