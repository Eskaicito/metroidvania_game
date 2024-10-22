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
    [SerializeField] private Image fadeImage;  

    private void Start()
    {
       
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }

        if (_connection == LevelConnection.ActiveConnecttion)
        {
            FindObjectOfType<PlayerMovement>().transform.position = _spawnPoint.position;
            StartCoroutine(FadeIn()); 
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
        
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);

            yield return StartCoroutine(FadeOut());
        }

        
        SceneManager.LoadScene(_targetZoneName);

        
        yield return null;

        
        var player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.transform.position = _spawnPoint.position;
        }

       
        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeIn());
            fadeImage.gameObject.SetActive(false);
            Destroy(fadeImage.gameObject);  
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
