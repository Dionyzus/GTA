using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC01 : MonoBehaviour
{
    [SerializeField] float theDistance;
    [SerializeField] GameObject actionDisplay;
    [SerializeField] GameObject actionText;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject textBox;
    [SerializeField] GameObject NPCName;
    [SerializeField] GameObject NPCText;
    [SerializeField] GameObject doorTrigger;
    [SerializeField] GameObject activeMissionName;
    [SerializeField] GameObject objective01;
    [SerializeField] GameObject missionTrigger;
    private bool flag;

    void Update()
    {
        theDistance = PlayerCast.distanceToTarget;
    }

    private void OnMouseOver()
    {
        if (flag)
        {
            return;
        }

        if (theDistance <= 0.05f)
        {
            ShooterBlock.blockShooter = true;
            actionText.GetComponent<TextMeshProUGUI>().text = "Talk";
            actionDisplay.SetActive(true);
            actionText.SetActive(true);

        }
        if (GameManager.Instance.InputController.Action)
        {
           
            if (theDistance <= 1)
            {
                actionDisplay.SetActive(false);
                actionText.SetActive(false);
                ShooterBlock.blockShooter = true;
                //thePlayer.SetActive(false);
                StartCoroutine(NPC01Active());
            }
        }
    }

    private void OnMouseExit()
    {
        ShooterBlock.blockShooter = false;
        actionDisplay.SetActive(false);
        actionText.SetActive(false);
    }

    IEnumerator NPC01Active()
    {
        if (MissionManager.ActiveMissionNumber == 2)
        {
            flag = true;
            textBox.SetActive(true);
            NPCName.GetComponent<TextMeshProUGUI>().text = "Izzy";
            NPCName.SetActive(true);
            NPCText.GetComponent<TextMeshProUGUI>().text = "Saznala sam gdje se nalazi Beli, u luci ima skladiste te vlastite pogone za trgovinu drogom.";
            NPCText.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            NPCText.SetActive(false);
            NPCText.GetComponent<TextMeshProUGUI>().text = "Pazi se, podrucje je jako dobro cuvano. Sretno.";
            NPCText.SetActive(true);
            yield return new WaitForSeconds(3);
            NPCName.SetActive(false);
            NPCText.SetActive(false);
            textBox.SetActive(false);
            doorTrigger.SetActive(true);
            MissionManager.SubMissionNumber = 3;
            StartCoroutine(SetMissionUI());
        }
        else
        {
            flag = true;

            textBox.SetActive(true);
            NPCName.GetComponent<TextMeshProUGUI>().text = "Izzy";
            NPCName.SetActive(true);
            NPCText.GetComponent<TextMeshProUGUI>().text = "Leone.Uskoro cu imati vise informacija.Vrati se kasnije.";
            NPCText.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            NPCName.SetActive(false);
            NPCText.SetActive(false);
            textBox.SetActive(false);
            flag = false;
        }

    }

    IEnumerator SetMissionUI()
    {
        
        activeMissionName.GetComponent<TextMeshProUGUI>().text = "Flush n Clean";
        objective01.GetComponent<TextMeshProUGUI>().text = "Kill Beli's bodyguard and abduct him.";
        MasterMission.MainMissionName = "Fishing";
        MasterMission.MainMissionInfo = "Kill all the guards in front of the warehouse where Beli is hiding. And abduct him.";
        yield return new WaitForSeconds(0.5f);
        activeMissionName.SetActive(true);

        yield return new WaitForSeconds(1);
        objective01.SetActive(true);

        yield return new WaitForSeconds(9);
        activeMissionName.SetActive(false);
        objective01.SetActive(false);
        missionTrigger.SetActive(false);
    }

}
