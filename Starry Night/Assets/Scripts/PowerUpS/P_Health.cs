using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Health : MonoBehaviour
{
    public void GivePowerUp(PlayerData player){
        player.health += 50;
    }
}
