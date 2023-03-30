using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    public float attack;
    public float health;
    public int levelReq;
    public Slot itemSlot;

    public enum Slot{
        Front,
        Mid,
        Back
    }
}
