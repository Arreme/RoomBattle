using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleState : RoombaState
{

    private float _invTime;
    private float _currentInvTime;
    private float _speed;
    private float _maxVel;
    private float _rotateSpeed;

    private MeshRenderer[] _mesh;

    public InvincibleState(float invTime, float speed, float maxVel, float rotateSpeed, MeshRenderer[] mesh)
    {
        _invTime = invTime;
        _currentInvTime = invTime;
        _speed = speed;
        _maxVel = maxVel;
        _rotateSpeed = rotateSpeed;
        _mesh = mesh;
        
    }

    public RoombaState runFrame(RoombaController controller)
    {
        _currentInvTime -= Time.deltaTime;
        controller._phy.MaxSpeed = _maxVel;
        controller._phy.addTorque(bisector(controller._inputVector, new Vector2(controller.transform.forward.x, controller.transform.forward.z)) * _rotateSpeed);
        controller._phy.addForce(controller._inputVector, _speed);
        
        if (_currentInvTime <= 0)
        {
            return controller._normalState;
        } else
        {
            return this;
        }
    }

    public RoombaState reset()
    {
        _currentInvTime = _invTime;
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
