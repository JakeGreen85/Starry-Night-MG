using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public float astroidSpawnRate;
    public float astroidSpeed;
    public float alienSpawnRate;
    public float alienSpeed;
    public float alienAttackSpeed;
    public double levelGoal;
    public int reward;
    public GameObject[] itemRewards;

    public GameObject RandomReward(){
        return itemRewards[Random.Range(0, itemRewards.Length)];
    }
}
