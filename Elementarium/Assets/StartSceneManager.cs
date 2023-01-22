using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Transform = UnityEngine.Transform;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private AudioSource source, quakeSource, playerFall;
    [SerializeField] private Transform environment;
    [SerializeField] private Image blackScreen;
    [SerializeField] private GameObject canvas;
    private float randomX, randomZ;
    public void PlayGame()
    {
        StartCoroutine(CoroutineStartGame());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator CoroutineStartGame()
    {
        canvas.SetActive(false);
        quakeSource.Play();
        source.Stop();
        StartCoroutine(CoroutineEarthQuake());
        yield return new WaitForSeconds(1);
        StartCoroutine(CoroutineBlackScreen());
    }

    IEnumerator CoroutineEarthQuake()
    {
        randomX = Random.Range(-0.15f, 0.15f);
        randomZ = Random.Range(-0.10f, 0.10f);
        environment.transform.position += new Vector3(randomX, 0, randomZ);
        yield return new WaitForSeconds(0.04f);
        StartCoroutine(CoroutineEarthQuake());
    }

    IEnumerator CoroutineBlackScreen()
    {
        while (blackScreen.color.a<=1)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                blackScreen.color.a + 0.075f);

            yield return new WaitForSeconds(0.05f);
            
            if (blackScreen.color.a>=0.5f && !playerFall.isPlaying)
            {
                playerFall.Play();
            }
        }
        
        quakeSource.Stop();

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
