using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Weapons : MonoBehaviour
{
    public void GivePowerUp(PlayerData player){
        player.attack *= 2;
    }
}
