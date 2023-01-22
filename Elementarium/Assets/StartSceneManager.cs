using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        source.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
