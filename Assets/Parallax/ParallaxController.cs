using UnityEngine;
using Cinemachine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;           // El transform de la capa
        public Vector2 parallaxEffectMultiplier;   // Multiplicador para el efecto de parallax
    }

    public ParallaxLayer[] layers;                 // Array que contiene todas las capas de parallax
    public bool infiniteHorizontal = true;
    public bool infiniteVertical = false;
    public bool useCinemachine = true;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float[] textureUnitSizeX;
    private float[] textureUnitSizeY;

    private void Start()
    {
        // Obtenemos la referencia a la cámara que sigue al jugador
        if (useCinemachine)
        {
            CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            cameraTransform = virtualCamera.Follow;
        }
        else
        {
            cameraTransform = Camera.main.transform;
        }

        lastCameraPosition = cameraTransform.position;

        // Inicializamos los arrays de tamaño de textura para cada capa
        textureUnitSizeX = new float[layers.Length];
        textureUnitSizeY = new float[layers.Length];

        // Para cada capa, calculamos el tamaño de su textura
        for (int i = 0; i < layers.Length; i++)
        {
            Sprite sprite = layers[i].layerTransform.GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;

            textureUnitSizeX[i] = (texture.width / sprite.pixelsPerUnit) * layers[i].layerTransform.localScale.x;
            textureUnitSizeY[i] = (texture.height / sprite.pixelsPerUnit) * layers[i].layerTransform.localScale.y;
        }
    }

    private void FixedUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Para cada capa aplicamos el parallax y ajustamos la posición
        for (int i = 0; i < layers.Length; i++)
        {
            // Aplicamos el parallax usando los multiplicadores de cada capa
            layers[i].layerTransform.position += new Vector3(deltaMovement.x * layers[i].parallaxEffectMultiplier.x, deltaMovement.y * layers[i].parallaxEffectMultiplier.y);

            // Comprobamos la repetición infinita en el eje X
            if (infiniteHorizontal && Mathf.Abs(cameraTransform.position.x - layers[i].layerTransform.position.x) >= textureUnitSizeX[i])
            {
                float offsetPositionX = (cameraTransform.position.x - layers[i].layerTransform.position.x) % textureUnitSizeX[i];
                layers[i].layerTransform.position = new Vector3(cameraTransform.position.x + offsetPositionX, layers[i].layerTransform.position.y);
            }

            // Comprobamos la repetición infinita en el eje Y
            if (infiniteVertical && Mathf.Abs(cameraTransform.position.y - layers[i].layerTransform.position.y) >= textureUnitSizeY[i])
            {
                float offsetPositionY = (cameraTransform.position.y - layers[i].layerTransform.position.y) % textureUnitSizeY[i];
                layers[i].layerTransform.position = new Vector3(layers[i].layerTransform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }

        lastCameraPosition = cameraTransform.position;
    }
}

