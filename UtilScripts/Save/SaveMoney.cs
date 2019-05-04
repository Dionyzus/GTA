using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMoney : MonoBehaviour
{
    [SerializeField] int loadMoney;

    void Start()
    {
        loadMoney = PlayerPrefs.GetInt("CashAmountSave");
    }
}
