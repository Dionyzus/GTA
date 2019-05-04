using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartQuest : MonoBehaviour
{
    [SerializeField] float theDistance;
    [SerializeField] GameObject actionDisplay;
    [SerializeField] GameObject actionText;
    [SerializeField] GameObject UIMission;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject missionCamera;
    [SerializeField] AudioController viewSound;
    [SerializeField] AudioController ringSound;
    [SerializeField] AudioController BGMusic;
    

    void Update()
    {
        theDistance = PlayerCast.distanceToTarget;
        if (ringSound && theDistance <= 2)
        {
            ringSound.Play();
        }
    }

    private void OnMouseOver()
    {
        if (theDistance <= 0.05f)
        {
            actionDisplay.SetActive(true);
            actionText.SetActive(true);
        }

        if (GameManager.Instance.InputController.Action)
        {
            if (theDistance <= 1)
            {
                ShooterBlock.blockShooter = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                viewSound.Play();
                BGMusic.Play();
                actionDisplay.SetActive(false);
                actionText.SetActive(false);
                ringSound.gameObject.SetActive(false);
                UIMission.SetActive(true);
                missionCamera.SetActive(true);
                thePlayer.SetActive(false);
                MasterMission.MainMissionName = "End Game";
                MasterMission.MainMissionInfo = "There should be supply box nearby with some heavy arms and enough ammo to clear guards. When I'm ready I should go to the marked point to meet people who will help me get to the Vladislav's mansion.";
                
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