using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Determina la següent posició del Roomba o d'un objecte amb una massa
 * determinada
 */
public class CustomPhysics : MonoBehaviour
{
    private float _mass = 1f;
    private float _linearDrag = 0.02f;
    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [SerializeField] private Vector2 _position = Vector2.zero;

    private float _maxSpeed = 0f;
    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set
        {
            _maxSpeed = value < 0 ? 0 : value;
        }
    }
    public float LinearDrag
    {
        get { return _linearDrag; }
        set
        {
            _linearDrag = value < 0 ? 0 : value;
        }
    }

    
    private void Awake()
    {
        _position = transform.position;
    }

    public void addForce(Vector2 direction,float speed)
    {
        //F = m * a, a = F/m
        Vector2 a = direction*speed / _mass;
        _velocity += finalVelocity(a);
        _velocity = Vector2.ClampMagnitude(_velocity,_maxSpeed);
        float linearDrag = Mathf.Exp(-_linearDrag * Time.deltaTime / _mass);
        _velocity = _velocity * linearDrag;
        _maxSpeed = Mathf.Max(10, Mathf.Min(_velocity.magnitude, _maxSpeed));
        _position += finalPosition(_velocity);
        transform.position = _position;
    }

    public Vector2 finalVelocity(Vector2 a)
    {
        //vel = v0 + a*t
        return a*Time.deltaTime;
    }

    public Vector2 finalPosition(Vector2 v)
    {
        //x = x0 + v*t
        return v * Time.deltaTime;
    }

    public bool linearDependency(Vector2 a, Vector2 b)
    {
        return (a.x * b.x) + (a.y * b.y) != 0;
    }

}
