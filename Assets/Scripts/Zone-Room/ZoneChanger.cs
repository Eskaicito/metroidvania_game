using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZoneChanger : MonoBehaviour
{
    [SerializeField] private LevelConnection _connection;
    [SerializeField] private string _targetZoneName;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Image fadeImage;  // Usaremos una imagen en lugar de un CanvasGroup

    private void Start()
    {
        // Asegúrate de que la imagen esté desactivada al inicio
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }

        if (_connection == LevelConnection.ActiveConnecttion)
        {
            FindObjectOfType<PlayerMovement>().transform.position = _spawnPoint.position;
            StartCoroutine(FadeIn()); // Fade in si es la escena correcta
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelConnection.ActiveConnecttion = _connection;
            StartCoroutine(TransitionToZone());
        }
    }

    private IEnumerator TransitionToZone()
    {
        // Activar la imagen negra y hacer el fade out
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);

            yield return StartCoroutine(FadeOut());
        }

        // Cargar la nueva zona
        SceneManager.LoadScene(_targetZoneName);

        // Esperar un frame para que la nueva escena cargue
        yield return null;

        // Reposicionar al jugador en la nueva zona
        var player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.transform.position = _spawnPoint.position;
        }

        // Hacer el fade in y luego desactivar la imagen
        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeIn());
            fadeImage.gameObject.SetActive(false);
            Destroy(fadeImage.gameObject);  // Destruir la imagen después del fade in
        }
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        Color color = fadeImage.color;
        while (timer < _connection.fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / _connection.fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = fadeImage.color;
        while (timer < _connection.fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / _connection.fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
