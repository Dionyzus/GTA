using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GlobalCash : MonoBehaviour
{

    [SerializeField] static int cashAmount;
    [SerializeField] int internalCashAmount;
    [SerializeField] GameObject cashDisplay;

    public static int CashAmount
    {
        get
        {
            return cashAmount;
        }

        set
        {
            cashAmount = value;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        internalCashAmount = CashAmount;
        cashDisplay.GetComponent<TextMeshProUGUI>().text = string.Format("{0}", internalCashAmount);
    }
}