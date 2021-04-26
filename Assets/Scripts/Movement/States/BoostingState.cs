using UnityEngine;

public class BoostingState : RoombaState
{
    private float boostTime;
    private float boostMaxSpeed;
    private float _currentTime;
    private Vector2 _direction;

    public BoostingState(float boostTime, float boostMaxSpeed)
    {
        this.boostTime = boostTime;
        this.boostMaxSpeed = boostMaxSpeed;
    }

    public RoombaState runFrame(RoombaController controller)
    {
        controller._phy.MaxSpeed = boostMaxSpeed;
        controller._phy.addTorque(bisector(_direction, new Vector2(controller.transform.forward.x, controller.transform.forward.z)) * 6);
        controller._phy.addForce(_direction,200);
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            return controller._normalState;
        } else 
        {
            return controller._boostingState;
        }
    }

    public RoombaState setUp(Vector2 direction)
    {
        _direction = direction;
        _currentTime = boostTime;
        return this;
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
