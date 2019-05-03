using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{

    [SerializeField] GameObject WinMenuPanel;

    [SerializeField] Button BackToMenuButton;

    void Start()
    {
        WinMenuPanel.SetActive(false);
        GameManager.Instance.EventBus.AddListener("OnAllEnemiesKilled", () =>
         {
             GameManager.Instance.Timer.Add(() =>
             {
                 GameManager.Instance.IsPaused = true;
                 WinMenuPanel.SetActive(true);
             }, 4);
         });

        BackToMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }

}
