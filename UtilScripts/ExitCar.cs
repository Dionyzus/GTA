using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class ExitCar : MonoBehaviour
{
    private GameObject carCamera;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject exitTrigger;
    [SerializeField] GameObject theCar;
    [SerializeField] GameObject exitPlace;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject enterCarTrigger;
    [SerializeField] GameObject carDoor;
    [SerializeField] GameObject mainCollider;
    [SerializeField] GameObject exitCarAnimationPosition;
    [SerializeField] GameObject playerGraphics;
    [SerializeField] GameObject lookAtPosition;
    [SerializeField] AudioController exitCarSound;
    [SerializeField] GameObject canvasTypeCar;
    [SerializeField] GameObject carMiniMap;

    private void Awake()
    {
        carCamera = GameObject.FindObjectOfType<RCC_Camera>().gameObject;
    }

    private void Update()
    {
        if (GameManager.Instance.InputController.exitCar)
        {
           
            carDoor.GetComponent<Animation>().Play("ExitCarDoor");

            theCar.GetComponent<RCC_CarControllerV3>().enabled = false;

            GameManager.Instance.Timer.Add(() =>
            {
                WaitDoor();
            }, 0.3f);
            exitCarSound.Play();

            GameManager.Instance.Timer.Add(() =>
            {
                ExecuteExitCar();

            }, 3.5f);

        }
    }
    public void ExecuteExitCar()
    {
        canvasTypeCar.SetActive(false);
        carCamera.SetActive(false);
        mainCamera.SetActive(true);
        carMiniMap.SetActive(false);
        exitTrigger.SetActive(false);

        enterCarTrigger.GetComponent<BoxCollider>().enabled = true;
        thePlayer.transform.LookAt(Vector3.forward);
        thePlayer.transform.position = exitPlace.transform.position;
        thePlayer.transform.rotation = Quaternion.Euler(0f, thePlayer.transform.eulerAngles.y, 0f);
        mainCollider.SetActive(true);
        EnterCar.InCar = false;

        thePlayer.GetComponent<CharacterController>().enabled = true;
        thePlayer.GetComponent<Player>().enabled = true;
        thePlayer.GetComponent<PlayerShoot>().enabled = true;

        EnterCar.CarTag = null;

    }
    public void WaitDoor()
    {
        playerGraphics.SetActive(true);
        thePlayer.transform.position = exitCarAnimationPosition.transform.position;
        thePlayer.transform.LookAt(lookAtPosition.transform);
    }
}