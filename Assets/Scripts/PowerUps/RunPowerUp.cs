using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPowerUp : PowerUp
{
    public RoombaState PowerUp(RoombaController controller)
    {
        controller._currentPowerUp = new NoPowerUp();
        Debug.Log("Ayo, powerup wasted");
        return controller._runPUState;
    }
}
