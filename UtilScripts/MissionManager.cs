using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] static int activeMissionNumber;
    [SerializeField] int internalMissionNumber;
    [SerializeField] GameObject mainMark;
    [SerializeField] GameObject obj01Mark;
    [SerializeField] GameObject obj02Mark;
    [SerializeField] GameObject obj03Mark;
    [SerializeField] static int subMissionNumber;
    [SerializeField] int internalSubNumber;
    [SerializeField] GameObject pointer;


    public static int ActiveMissionNumber
    {
        get
        {
            return activeMissionNumber;
        }

        set
        {
            activeMissionNumber = value;
        }
    }

    public int InternalQuestNumber
    {
        get
        {
            return internalMissionNumber;
        }

        set
        {
            internalMissionNumber = value;
        }
    }

    public static int SubMissionNumber
    {
        get
        {
            return subMissionNumber;
        }

        set
        {
            subMissionNumber = value;
        }
    }

    private void Update()
    {

        InternalQuestNumber = ActiveMissionNumber;
        internalSubNumber = SubMissionNumber;
        pointer.transform.LookAt(mainMark.transform);

        if (EnterCar.CarTag != null)
        {
            EnterCar.CarTag.transform.Find("CarPointer").transform.LookAt(mainMark.transform);
        }

        if (internalSubNumber == 0)
        {
            pointer.SetActive(false);
            if (EnterCar.CarTag != null)
            {
                EnterCar.CarTag.transform.Find("CarPointer").gameObject.SetActive(false);
            }
        }

        else
        {
            if (EnterCar.InCar)
            {
                EnterCar.CarTag.transform.Find("CarPointer").gameObject.SetActive(true);
                pointer.SetActive(false);
            }
            else
            {
                if (EnterCar.CarTag != null)
                {
                    EnterCar.CarTag.transform.Find("CarPointer").gameObject.SetActive(false);
                }
                pointer.SetActive(true);
            }
        }

        if (internalSubNumber == 1)
        {
            mainMark.transform.position = obj01Mark.transform.position;
        }
        if (internalSubNumber == 2)
        {
            mainMark.transform.position = obj02Mark.transform.position;
        }
        if (internalSubNumber == 3)
        {
            mainMark.transform.position = obj03Mark.transform.position;
        }
    }
}