using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KarmaSystem : MonoBehaviour
{
    public int redencionPoints { get; private set; }
    public int venganzaPoints { get; private set; }

    // Aumentar puntos de redención
    public void AddRedencionPoints(int amount)
    {
        redencionPoints += amount;
        Debug.Log("Redención aumentada. Puntos actuales: " + redencionPoints);
    }

    // Aumentar puntos de venganza
    public void AddVenganzaPoints(int amount)
    {
        venganzaPoints += amount;
        Debug.Log("Venganza aumentada. Puntos actuales: " + venganzaPoints);
    }
}
