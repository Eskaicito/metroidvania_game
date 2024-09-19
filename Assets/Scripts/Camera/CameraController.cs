using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public Transform playerTransform;
    public float minYSpeed = -5f;
    public float maxYSpeed = 5f;
    public float maxDamping = 3f;
    public float minDamping = 0.5f;
    public float transitionTime = 0.3f;

    private CinemachineFramingTransposer framingTransposer;
    private Rigidbody2D playerRigidbody;

    private CinemachineBasicMultiChannelPerlin noise; // Para el shake

    private void Start()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();

        // Obtener el componente de ruido para el "shake"
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Verificar si el componente de ruido existe
        if (noise == null)
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin component not found on the virtual camera.");
        }
        else
        {
            // Asegurarse de que el "shake" no est� activo al inicio
            noise.m_AmplitudeGain = 0f;
        }
    }

    private void Update()
    {
        AdjustDampingBasedOnSpeed();
    }

    void AdjustDampingBasedOnSpeed()
    {
        float playerYSpeed = playerRigidbody.velocity.y;
        float targetDamping = Mathf.Lerp(minDamping, maxDamping, Mathf.InverseLerp(minYSpeed, maxYSpeed, Mathf.Abs(playerYSpeed)));
        framingTransposer.m_YDamping = Mathf.Lerp(framingTransposer.m_YDamping, targetDamping, Time.deltaTime / transitionTime);
    }

    // Funci�n para activar el "shake" de la c�mara
    public void ShakeCamera()
    {
        if (noise != null)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        if (noise != null)
        {
            // Ajustar la intensidad del shake
            noise.m_AmplitudeGain = 2f;  // Ajusta seg�n lo que necesites
            yield return new WaitForSeconds(0.1f);  // Duraci�n del shake
            noise.m_AmplitudeGain = 0f;  // Apagar el shake
        }
    }
}
