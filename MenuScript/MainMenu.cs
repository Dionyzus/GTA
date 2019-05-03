using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField] Button StartGameButton;
    [SerializeField] Button QuitGameButton;

    public string levelName;

    private void Start()
    {
        StartGameButton.onClick.AddListener(()=>
        {
            StartGame(levelName);
        });

        QuitGameButton.onClick.AddListener(QuitGame);
    }

    public void StartGame(string name)
    {
        SceneManager.LoadScene("StartingScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
