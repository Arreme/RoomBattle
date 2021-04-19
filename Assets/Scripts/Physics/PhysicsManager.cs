using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    private List<IPhysicalObject> _world;
    private void Awake()
    {
        _world = new List<IPhysicalObject>();
    }
    public void PhysicsSubscribe(IPhysicalObject physicObj)
    {
        _world.Add(physicObj);
    }

    public void PhysicsUnsubscribe(IPhysicalObject physicObj)
    {
        _world.Remove(physicObj);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(IPhysicalObject obj in _world)
        {
            obj.Step();
        }
    }
}
