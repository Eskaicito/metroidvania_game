using UnityEngine;
using UnityEngine.UI;

public class KarmaSystem : MonoBehaviour
{
    public int redencionPoints { get; private set; }
    public int venganzaPoints { get; private set; }

    [SerializeField] private Slider karmaSlider; 
    [SerializeField] private Color neutralColor = Color.yellow; 

    private int maxKarmaPoints = 40; 

    private void Start()
    {
       
        karmaSlider.minValue = -maxKarmaPoints;
        karmaSlider.maxValue = maxKarmaPoints;
        karmaSlider.value = 0;  


        UpdateKarmaBar();
    }

    public void AddRedencionPoints(int amount)
    {
        redencionPoints += amount;
        redencionPoints = Mathf.Clamp(redencionPoints, 0, maxKarmaPoints);
        Debug.Log("Puntos de Redención: " + redencionPoints);  
        UpdateKarmaBar();
    }

    public void AddVenganzaPoints(int amount)
    {
        venganzaPoints += amount;
        venganzaPoints = Mathf.Clamp(venganzaPoints, 0, maxKarmaPoints);
        Debug.Log("Puntos de Venganza: " + venganzaPoints); 
        UpdateKarmaBar();
    }

    private void UpdateKarmaBar()
    {
        
        int balance = redencionPoints - venganzaPoints;

    
        karmaSlider.value = balance;

       
    }
}
