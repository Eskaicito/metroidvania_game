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
        if (_connection == LevelConnection.ActiveConnecttion)
        {
            FindObjectOfType<PlayerMovement>().transform.position = _spawnPoint.position;
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
        // Fundido a negro si está habilitado
        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        // Cargar pantalla de carga si está definida
        if (!string.IsNullOrEmpty(_connection.loadingScreenSceneName))
        {
            SceneManager.LoadScene(_connection.loadingScreenSceneName);
            yield return null;  // Esperar un frame para cargar
        }

        // Cargar la nueva zona
        SceneManager.LoadScene(_targetZoneName);

        // Fundido desde negro al cargar la nueva zona
        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeIn());
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
