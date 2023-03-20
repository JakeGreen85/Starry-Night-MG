using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Money : MonoBehaviour
{
    public void GivePowerUp(PlayerData player){
        player.money += 100;
    }
}
