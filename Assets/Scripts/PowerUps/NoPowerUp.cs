using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerUp : PowerUp
{
    public RoombaState runPowerUp(RoombaController controller)
    {
        Debug.Log("No power up!");
        return controller._normalState;
    }
}
