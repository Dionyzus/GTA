using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using TMPro;


public class EnterCar : MonoBehaviour
{
    private GameObject carCamera;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject exitTrigger;
    [SerializeField] GameObject theCar;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject enterCarTrigger;
    [SerializeField] GameObject carDoor;
    [SerializeField] GameObject mainCollider;
    [SerializeField] GameObject animationPositionStart;
    [SerializeField] GameObject playerGraphics;
    [SerializeField] GameObject lookAtAngle;
    [SerializeField] AudioController enterCarSound;
    [SerializeField] GameObject actionEnter;
    [SerializeField] GameObject actionLeave;
    [SerializeField] GameObject canvasTypeCar;
    [SerializeField] GameObject carMiniMap;

    bool triggerCheck;

    static GameObject carTag;
    static bool canEnter;
    static bool inCar;

    public static bool InCar
    {
        get
        {
            return inCar;
        }

        set
        {
            inCar = value;
        }
    }

    public static bool CanEnter
    {
        get
        {
            return canEnter;
        }

        set
        {
            canEnter = value;
        }
    }

    public static GameObject CarTag
    {
        get
        {
            return carTag;
        }

        set
        {
            carTag = value;
        }
    }

    public void OnTriggerEnter(Collider collider)
    {

        if (inCar)
        {
            return;
        }
        if (collider.tag == "Player")
        {
            triggerCheck = true;
            canEnter = true;
            actionLeave.SetActive(true);
            actionEnter.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        triggerCheck = false;
        canEnter = false;
        actionLeave.SetActive(false);
        actionEnter.SetActive(false);
    }
    private void Awake()
    {
        carCamera = GameObject.FindObjectOfType<RCC_Camera>().gameObject;
    }

    private void Update()
    {
        if(!inCar)
        {
            carTag = null;
        }

        if (inCar)
        {
            actionLeave.SetActive(false);
            actionEnter.SetActive(false);
        }

        if (PauseMenu.IsPaused)
        {
            actionLeave.SetActive(false);
            actionEnter.SetActive(false);
            carCamera.SetActive(false);
            canvasTypeCar.SetActive(false);
        }

        else if (!PauseMenu.IsPaused && inCar)
        {
            actionLeave.SetActive(false);
            actionEnter.SetActive(false);
            carCamera.SetActive(true);
            canvasTypeCar.SetActive(true);
        }

        if (triggerCheck && !inCar)
        {
            if (GameManager.Instance.InputController.enterCar)
            {
                thePlayer.transform.position = animationPositionStart.transform.position;
                thePlayer.transform.LookAt(lookAtAngle.transform);
                carDoor.GetComponent<Animation>().Play("OpenCarDoor");
                enterCarSound.Play();

                thePlayer.GetComponent<CharacterController>().enabled = false;
                thePlayer.GetComponent<Player>().enabled = false;
                thePlayer.GetComponent<PlayerShoot>().enabled = false;

                mainCollider.SetActive(false);
                GameManager.Instance.Timer.Add(() =>
                {
                    ExecuteEnterCar();

                }, 3);
            }
        }
    }

    public void ExecuteEnterCar()
    {
        canvasTypeCar.SetActive(true);
        playerGraphics.SetActive(false);
        mainCamera.SetActive(false);
        carCamera.SetActive(true);
        carMiniMap.SetActive(true);

        if (carCamera.GetComponent<RCC_Camera>())
        {
            carCamera.GetComponent<RCC_Camera>().cameraSwitchCount = 10;
            carCamera.GetComponent<RCC_Camera>().ChangeCamera();
        }
        carCamera.transform.GetComponent<RCC_Camera>().SetPlayerCar(theCar);

        theCar.GetComponent<RCC_CarControllerV3>().enabled = true;

        inCar = true;
        enterCarTrigger.GetComponent<BoxCollider>().enabled = false;

        carTag = theCar;
        exitTrigger.SetActive(true);
    }

}