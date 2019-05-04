using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameObj2 : MonoBehaviour
{
    [SerializeField] GameObject theObjective;
    [SerializeField] int closeObjective;
    [SerializeField] GameObject missionPoint;

    private void Update()
    {
        if (closeObjective == 2)
        {
            if (theObjective.transform.localScale.y <= 0.0f)
            {
                closeObjective = 0;
                theObjective.SetActive(false);
                missionPoint.SetActive(false);
            }
            else
            {
                theObjective.transform.localScale -= new Vector3(0.0f, 0.01f, 0.0f);
            }
        }
    }

    private void OnTriggerEnter()
    {
        MissionManager.SubMissionNumber = 0;
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(FinishObjective());
    }

    IEnumerator FinishObjective()
    {
        theObjective.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GlobalCash.CashAmount += 100;
        PlayerPrefs.SetInt("CashAmountSave", GlobalCash.CashAmount);
        MissionManager.ActiveMissionNumber = 2;
        MasterMission.MainMissionName = "";
        MasterMission.MainMissionInfo = "";
        closeObjective = 2;
    }
}
