using UnityEngine;

public class NormalState : RoombaState
{
    public float _speed;
    public float _maxVel;


    public RoombaState runFrame(RoombaController controller)
    {
        controller._phy.MaxSpeed = _maxVel;
        controller._phy.addTorque(bisector(controller._inputVector,new Vector2(controller.transform.forward.x,controller.transform.forward.z)));
        if (controller._boosting)
        {
            return controller._boostingState;
        } else
        {
            return controller._normalState;
        }
    }

    private Vector2 bisector(Vector2 a, Vector2 b)
    {
        if ((a.x * b.x) + (a.y * b.y) != 0)
        {
            a += new Vector2(0.0001f, 0.0001f);
        }
        return (b.magnitude * a + a.magnitude * b).normalized;
    }
}
