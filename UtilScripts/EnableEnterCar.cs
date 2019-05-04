using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnterCar : MonoBehaviour
{
    [SerializeField] GameObject trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger.GetComponent<EnterCar>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger.GetComponent<EnterCar>().enabled = false;
        }
    }
}