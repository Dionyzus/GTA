using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject EscapeMenuPanel;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    [SerializeField] Button missionButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject playerHealth;
    [SerializeField] GameObject carCamera;

    static bool isPaused;
    public GameObject MenuCamera;
    public GameObject Camera;

    //Turn off objects when game is paused
    [SerializeField] GameObject assaultWeaponBullet;
    [SerializeField] GameObject shotgunBullet;
    [SerializeField] GameObject AK47;
    [SerializeField] GameObject M4;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject inventory; 
    [SerializeField] GameObject cash;
    [SerializeField] GameObject miniMap;
    [SerializeField] GameObject slider;
    [SerializeField] GameObject title;
    [SerializeField] GameObject escapeWindow;
    [SerializeField] GameObject missionPanel;

    public static bool IsPaused
    {
        get
        {
            return isPaused;
        }

        set
        {
            isPaused = value;
        }
    }

    void Start()
    {
        //EscapeMenuPanel.SetActive(false);
        title.SetActive(false);
        slider.SetActive(false);
        missionPanel.SetActive(false);
        YesButton.onClick.AddListener(OnYesClicked);
        NoButton.onClick.AddListener(OnNoClicked);
        missionButton.onClick.AddListener(OnMissionClicked);
    }

    void OnMissionClicked()
    {
        //missionPanel.GetComponent<Animation>().Play("MissionSlideInfo");
        missionPanel.SetActive(true);
    }

    void OnYesClicked()
    {
        EscapeMenuPanel.SetActive(false);
        title.SetActive(false);
        escapeWindow.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("NewMainMenu");
    }

    void OnNoClicked()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.IsPaused = false;
        //EscapeMenuPanel.SetActive(false);
        slider.SetActive(false);
        title.SetActive(false);
        MenuCamera.SetActive(false);
        assaultWeaponBullet.SetActive(true);
        shotgunBullet.SetActive(true);
        AK47.SetActive(true);
        M4.SetActive(true);
        shotgun.SetActive(true);
        inventory.SetActive(true);
        cash.SetActive(true);
        miniMap.SetActive(true);
        playerHealth.SetActive(true);
        isPaused = false;
        if(EnterCar.InCar)
        {
            return;
        }
        carCamera.SetActive(true);
        Camera.SetActive(true);
    }

    void Update()
    {
        if (slider.activeSelf)
        {
            return;
        }

        if (GameManager.Instance.InputController.Escape)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.Instance.IsPaused = true;

            Camera.SetActive(false);
            carCamera.SetActive(false);
            assaultWeaponBullet.SetActive(false);
            shotgunBullet.SetActive(false);
            AK47.SetActive(false);
            M4.SetActive(false);
            shotgun.SetActive(false);
            inventory.SetActive(false);
            cash.SetActive(false);
            miniMap.SetActive(false);
            playerHealth.SetActive(false);
            MenuCamera.SetActive(true);
            slider.SetActive(true);
            slider.GetComponent<Animation>().Play("ButtonSideSlide");
            title.SetActive(true);
            isPaused = true;
            //EscapeMenuPanel.SetActive(true);
           

        }
    }
}