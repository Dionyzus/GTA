using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameObj1 : MonoBehaviour
{
    [SerializeField] float theDistance;
    [SerializeField] GameObject supplyBox;
    [SerializeField] GameObject actionDisplay;
    [SerializeField] GameObject actionText;
    [SerializeField] GameObject theObjective;
    [SerializeField] int closeObjective;
    [SerializeField] GameObject collectable;
    [SerializeField] GameObject items;

    [SerializeField] GameObject missionTrigger;

    // Update is called once per frame
    void Update()
    {
        theDistance = PlayerCast.distanceToTarget;

        if (closeObjective == 1)
        {
            if (theObjective.transform.localScale.y <= 0.0f)
            {
                closeObjective = 0;
                theObjective.SetActive(false);
                missionTrigger.SetActive(false);
            }
            else
            {
                theObjective.transform.localScale -= new Vector3(0.0f, 0.01f, 0.0f);
            }
        }
    }

    private void OnMouseOver()
    {
        if (theDistance <= 0.05f)
        {
            actionText.GetComponent<TextMeshProUGUI>().text = "Open crate";
            actionText.SetActive(true);
            actionDisplay.SetActive(true);
        }

        if (GameManager.Instance.InputController.Action)
        {
            if (theDistance <= 1)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                supplyBox.GetComponent<Animation>().Play("opened_closed");
                collectable.SetActive(true);
                actionText.SetActive(false);
                actionDisplay.SetActive(false);
                MissionManager.SubMissionNumber = 2;
                StartCoroutine(FinishObjective());

            }
        }
    }

    private void OnMouseExit()
    {
        actionDisplay.SetActive(false);
        actionText.SetActive(false);
          
    }

    IEnumerator FinishObjective()
    {
        yield return new WaitForSeconds(0.75f);
        items.SetActive(false);
        theObjective.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        collectable.SetActive(false);
        closeObjective = 1;
    }
}
