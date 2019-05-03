using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour {

    [SerializeField] GameObject EscapeMenuPanel;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    [SerializeField] Button SkipCutscene;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject ambienceSounds;

    void Start()
    {
        EscapeMenuPanel.SetActive(false);
        YesButton.onClick.AddListener(OnYesClicked);
        NoButton.onClick.AddListener(OnNoClicked);
        SkipCutscene.onClick.AddListener(OnSkipCutscene);
    }

    void OnYesClicked()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("NewMainMenu");
    }

    void OnNoClicked()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.IsPaused = false;
        EscapeMenuPanel.SetActive(false);
    }

    void OnSkipCutscene()
    {
        ambienceSounds.SetActive(false);
        loadingScreen.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(3);
    }

    void Update()
    {
        if(EscapeMenuPanel.activeSelf)
        {
            return;
        }

        if(GameManager.Instance.InputController.Escape)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.Instance.IsPaused = true;
            EscapeMenuPanel.SetActive(true);
        }
    }
}
