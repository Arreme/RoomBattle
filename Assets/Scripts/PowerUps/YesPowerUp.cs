using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesPowerUp : PowerUp
{
    public RoombaState runPowerUp(RoombaController controller)
    {
        controller._currentPowerUp = new NoPowerUp();
        Debug.Log("Ayo, powerup wasted");
        return controller._normalState;
    }
}
