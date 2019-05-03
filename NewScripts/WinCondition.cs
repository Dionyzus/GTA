using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    [SerializeField] Destructable[] targets;
    [SerializeField] int closeObjective;
    [SerializeField] GameObject theObjective;


    int targetsDestroyedCounter;

    void Start()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].OnDeath += WinCondition_OnDeath;
        }
    }
    private void Update()
    {

        if (closeObjective == 3)
        {
            if (theObjective.transform.localScale.y <= 0.0f)
            {
                closeObjective = 0;
                theObjective.SetActive(false);
            }
            else
            {
                theObjective.transform.localScale -= new Vector3(0.0f, 0.01f, 0.0f);
            }
        }
    }

    private void WinCondition_OnDeath()
    {
        targetsDestroyedCounter++;

        if (targetsDestroyedCounter == targets.Length)
        {
            StartCoroutine(FinishObjective());
            MissionManager.SubMissionNumber = 0;
            MissionManager.ActiveMissionNumber = 3;

        }
    }
    IEnumerator FinishObjective()
    {
        yield return new WaitForSeconds(0.75f);
        theObjective.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        MasterMission.MainMissionName = "";
        MasterMission.MainMissionInfo = "";
        closeObjective = 3;
    }
}