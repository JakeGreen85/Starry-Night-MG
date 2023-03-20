using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Invinsible : MonoBehaviour
{
    public void GivePowerUp(PlayerData player){
        player.GetComponent<BoxCollider2D>().enabled = false;
    }
}
