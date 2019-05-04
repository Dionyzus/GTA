using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] AudioController BGMusic;
    [SerializeField] AudioController ringSound;
        
    [SerializeField] GameObject questHalo;
    [SerializeField] GameObject UIQuest;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject missionCamera;
    [SerializeField] GameObject ammoTrigger;
    [SerializeField] GameObject missionPoint;

    [SerializeField] GameObject activeQuestText;
    [SerializeField] GameObject objective01;
    [SerializeField] GameObject objective02;

    public void AcceptMission()
    {
        MissionManager.SubMissionNumber = 1;
        thePlayer.SetActive(true);
        missionCamera.SetActive(false);
        UIQuest.SetActive(false);
        questHalo.SetActive(false);
        BGMusic.Stop();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(SetQuestUI());

    }
    
    public void DeclineMission()
    {
        thePlayer.SetActive(true);
        missionCamera.SetActive(false);
        UIQuest.SetActive(false);
        BGMusic.Stop();
        ringSound.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    IEnumerator SetQuestUI()
    {
        activeQuestText.GetComponent<TextMeshProUGUI>().text = "End Game";
        objective01.GetComponent<TextMeshProUGUI>().text = "Collect weapons and ammo crates.";
        objective02.GetComponent<TextMeshProUGUI>().text = "Head to the marked point.";
        MissionManager.ActiveMissionNumber = 1;
        ammoTrigger.SetActive(true);
        missionPoint.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        activeQuestText.SetActive(true);

        yield return new WaitForSeconds(1);
        objective01.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        objective02.SetActive(true);

        yield return new WaitForSeconds(9);
        activeQuestText.SetActive(false);
        objective01.SetActive(false);
        objective02.SetActive(false);

    }
}
