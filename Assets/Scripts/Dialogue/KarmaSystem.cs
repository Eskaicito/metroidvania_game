using UnityEngine;
using UnityEngine.UI;

public class KarmaSystem : MonoBehaviour
{
    public int redencionPoints { get; private set; }
    public int venganzaPoints { get; private set; }

    [SerializeField] private Slider karmaSlider;  // Barra de karma en el UI
    [SerializeField] private Image fillImage;     // Imagen de la barra (para cambiar el color)
    [SerializeField] private Color neutralColor = Color.yellow;  // El color neutro ahora será amarillo

    private int maxKarmaPoints = 40;  // Máximo de puntos para cada lado (redención y venganza)

    private void Start()
    {
        // Inicia la barra en el centro con el color neutro (amarillo)
        karmaSlider.minValue = -maxKarmaPoints;
        karmaSlider.maxValue = maxKarmaPoints;
        karmaSlider.value = 0; // Empieza en equilibrio

        fillImage.color = neutralColor;  // Iniciar con color amarillo
        UpdateKarmaBar();
    }

    // Aumentar puntos de redención
    public void AddRedencionPoints(int amount)
    {
        redencionPoints += amount;
        redencionPoints = Mathf.Clamp(redencionPoints, 0, maxKarmaPoints);
        UpdateKarmaBar();
    }

    // Aumentar puntos de venganza
    public void AddVenganzaPoints(int amount)
    {
        venganzaPoints += amount;
        venganzaPoints = Mathf.Clamp(venganzaPoints, 0, maxKarmaPoints);
        UpdateKarmaBar();
    }

    // Actualizar la barra de karma según los puntos actuales
    private void UpdateKarmaBar()
    {
        // Calcular el balance de karma como la diferencia entre redención y venganza
        int balance = redencionPoints - venganzaPoints;

        // Actualiza el valor del slider basado en el balance
        karmaSlider.value = balance;

        // Cambia el color de la barra solo si la venganza es mayor
        if (balance < 0)
        {
            fillImage.color = new Color(0.5f, 0, 0.5f);  // Más venganza (morado)
        }
        else
        {
            fillImage.color = neutralColor;  // Mantener color amarillo para redención o equilibrio
        }
    }
}
