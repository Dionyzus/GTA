using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] AudioController backgroundMusic;
    [SerializeField] GameObject loadingScreen;

    public void PlayGame()
    {
        backgroundMusic.Stop();
        loadingScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}