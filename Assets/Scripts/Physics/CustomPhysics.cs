using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Determina la següent posició del Roomba o d'un objecte amb una massa
 * determinada
 */
public class CustomPhysics : MonoBehaviour, IPhysicalObject
{
    [SerializeField] private PhysicsManager _manager;

    private Rigidbody2D _rigibody;

    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [SerializeField] private Vector3 _position = Vector3.zero;
    [SerializeField] private Vector2 _force = Vector2.zero;
    
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _linearDrag = 0.02f;
    [SerializeField] private float _maxSpeed = 0f;


    private Torque _angularForce;
    [SerializeField] private float _angularSpeed = 0f;
    [SerializeField] private float _angularAcceleration = 0f;
    [SerializeField] private float _angularDrag = 0.05f;
    [SerializeField] private float _angularDisplacement = 0f;

    public Vector2 movementRestriction = Vector2.zero;
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
    public float AngularDrag {
        get { return _angularDrag; }
        set
        {
            _angularDrag = value < 0 ? 0 : value;
        }
    }

    private void Start()
    {
        _position = transform.position;
        _angularDisplacement = transform.rotation.eulerAngles.z;
        _manager.PhysicsSubscribe(this);
        _rigibody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Step()
    {
        LinearMovement();
        AngularMovement();
    }

    #region(AngularMovement)
    private void AngularMovement()
    {
        _angularAcceleration = _angularForce.force.magnitude * Vector2.Dot(_angularForce.force, -transform.right) / _mass * _angularForce.radius;
        _angularForce.force = Vector2.zero;

        _angularSpeed += angularVelocity(_angularAcceleration);

        _angularSpeed -= _angularSpeed * _angularDrag;

        _angularDisplacement += angularMovement(_angularSpeed);

        transform.Rotate(new Vector3(0, 0, _angularAcceleration));
    }

    private float angularMovement(float angularSpeed)
    {
        return Mathf.Rad2Deg * angularSpeed * Time.deltaTime;
    }

    private float angularVelocity(float angularAcceleration)
    {
        return angularAcceleration * Time.deltaTime;
    }
    public void addTorque(Vector2 force, float radius)
    {
        _angularForce = new Torque(force, radius);
    }

    struct Torque
    {
        public Vector2 force;
        public float radius;

        public Torque(Vector2 force, float radius)
        {
            this.force = force;
            this.radius = radius;
        }
    }

    #endregion

    #region(LinearVelocity)

    private void LinearMovement()
    {
        Vector2 a = _force / _mass;
        _force = Vector2.zero;

        _velocity += finalVelocity(a);
        _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);
        float linearDrag = Mathf.Exp(-_linearDrag * Time.deltaTime / _mass);
        _velocity = _velocity * linearDrag;
        _maxSpeed = Mathf.Max(10, Mathf.Min(_velocity.magnitude, _maxSpeed));
        if (movementRestriction != Vector2.zero)
        {
            float dotX = Vector2.Dot(movementRestriction, new Vector2(_velocity.x, 0));
            float dotY = Vector2.Dot(movementRestriction, new Vector2(0, _velocity.y));
            _velocity = new Vector2(dotX, dotY);
            Debug.Log(_velocity);
        }

        _position += finalPosition(_velocity);
        Debug.Log(_position);
        transform.position = _position;
    }

    public void addForce(Vector2 direction,float newtons)
    {
        _force = direction * newtons;
    }

    
    public Vector2 finalVelocity(Vector2 a)
    {
        //vel = v0 + a*t
        return a*Time.deltaTime;
    }

    public Vector3 finalPosition(Vector2 v)
    {
        //x = x0 + v*t
        return v * Time.deltaTime;
    }
    #endregion

    public bool linearDependency(Vector2 a, Vector2 b)
    {
        return (a.x * b.x) + (a.y * b.y) != 0;
    }
    

}
