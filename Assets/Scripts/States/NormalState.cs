using UnityEngine;

public class NormalState : RoombaState
{
    private float _speed;
    private float _maxVel;
    private float _rotateSpeed;

    public NormalState(float speed, float maxVel, float rotateSpeed, MeshRenderer[] mesh)
    {
        _speed = speed;
        _maxVel = maxVel;
        _rotateSpeed = rotateSpeed;
    }

    public RoombaState runFrame(RoombaController controller)
    {
        controller._phy.MaxSpeed = _maxVel;
        controller._phy.addTorque(bisector(controller._inputVector,new Vector2(controller.transform.forward.x,controller.transform.forward.z))*_rotateSpeed);
        controller._phy.addForce(controller._inputVector, _speed);
        if (controller._boosting)
        {
            return controller._boostingState.setUp(new Vector2(controller.transform.forward.x, controller.transform.forward.z));
        } else if (controller._powerUp)
        {
            return controller._currentPowerUp.PowerUp(controller);
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
