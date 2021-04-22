using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RoombaState
{
    public RoombaState runFrame(RoombaController controller);
}
