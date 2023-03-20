using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_InstaKill : MonoBehaviour
{
    public void GivePowerUp(PlayerData player){
        foreach(GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")){
            Destroy(astroid);
        }
    }
}
