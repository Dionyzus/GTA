using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Soldier",menuName ="Data/Soldier")]
public class Soldier : ScriptableObject {

    public float RunSpeed;
    public float WalkSpeed;
    public float CrouchSpeed;
    public float SneakSpeed;
}
