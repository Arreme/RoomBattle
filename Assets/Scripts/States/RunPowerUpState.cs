using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPowerUpState : RoombaState
{
    private float powerUpTime;
    private float powerUpSpeed;
    private float rotateSpeed;
    private float _currentTime;

    public RunPowerUpState(float powerUpTime, float powerUpSpeed, float rotateSpeed ,MeshRenderer[] mesh)
    {
        this.powerUpTime = powerUpTime;
        this.powerUpSpeed = powerUpSpeed;
        this.rotateSpeed = rotateSpeed;
        _currentTime = powerUpTime;
    }

    public RoombaState runFrame(RoombaController controller)
    {
        controller._phy.MaxSpeed = powerUpSpeed;
        controller._phy.addTorque(bisector(controller._inputVector,new Vector2(controller.transform.forward.x,controller.transform.forward.z))*rotateSpeed);
        controller._phy.addForce(controller._inputVector, powerUpSpeed);
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = powerUpTime;
            return controller._normalState;
        } else 
        {
            return controller._runPUState;
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
