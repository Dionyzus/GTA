using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MasterMission : MonoBehaviour
{
    [SerializeField] GameObject mainMissionText;
    [SerializeField] GameObject mainMissionDescription;
    [SerializeField] static string mainMissionName;
    [SerializeField] static string mainMissionInfo;

    public static string MainMissionName
    {
        get
        {
            return mainMissionName;
        }

        set
        {
            mainMissionName = value;
        }
    }

    public static string MainMissionInfo
    {
        get
        {
            return mainMissionInfo;
        }

        set
        {
            mainMissionInfo = value;
        }
    }


    // Update is called once per frame
    void Update()
    {
        mainMissionText.GetComponent<TextMeshProUGUI>().text = MainMissionName;
        mainMissionDescription.GetComponent<TextMeshProUGUI>().text = MainMissionInfo;
    }
}
