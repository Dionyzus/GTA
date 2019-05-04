using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionFishingStart : MonoBehaviour
{
    [SerializeField] float theDistance;
    [SerializeField] GameObject actionDisplay;
    [SerializeField] GameObject actionText;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject targets;


    void Update()
    {
        theDistance = PlayerCast.distanceToTarget;
    }
    private void OnMouseOver()
    {
        if (theDistance <= 0.05f)
        {
            actionText.GetComponent<TextMeshProUGUI>().text = "Exit Villa";
            actionDisplay.SetActive(true);
            actionText.SetActive(true);
        }

        if (GameManager.Instance.InputController.Action)
        {
            if (theDistance <= 1)
            {
                GetComponent<BoxCollider>().enabled = false;
                ShooterBlock.blockShooter = true;
                actionDisplay.SetActive(false);
                actionText.SetActive(false);
                leftDoor.GetComponent<Animation>().Play("LeftGateOpen");
                rightDoor.GetComponent<Animation>().Play("OpenGate");
                targets.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        ShooterBlock.blockShooter = false;
        actionDisplay.SetActive(false);
        actionText.SetActive(false);
    }

   
}