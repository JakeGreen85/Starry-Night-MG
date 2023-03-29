using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public string ID;
    public float attack;
    public float health;
    public float maxHealth;
    public int money;
    public int level;
    public double endlessHighscore;
    public int towerFloor;
    public GameObject[] inventory = new GameObject[20];
    public GameObject[] equipped = new GameObject[5];
}
