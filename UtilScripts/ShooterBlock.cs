using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBlock : MonoBehaviour
{

    public static bool blockShooter;
    public bool internalBlock;

    void Update()
    {
        internalBlock = blockShooter;
    }
}
