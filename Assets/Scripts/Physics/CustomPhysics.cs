using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Determina la següent posició del Roomba o d'un objecte amb una massa
 * determinada
 */
public class CustomPhysics : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private Vector2 _force = Vector2.zero;
    [SerializeField] private float _maxSpeed = 0f;
    private Vector2 _angularForce;

    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set
        {
            _maxSpeed = value < 0 ? 0 : value;
        }
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.position = transform.position;
        _rb.rotation = transform.rotation.eulerAngles.z;
        _rb.centerOfMass = Vector2.zero;
    }

    public void FixedUpdate()
    {
        LinearMovement();
        AngularMovement();
    }

    #region(AngularMovement)
    private void AngularMovement()
    {
        if (_angularForce != Vector2.zero)
        {
            _rb.angularVelocity = _angularForce.magnitude * Vector2.Dot(_angularForce, -transform.right) / _rb.mass;
            _angularForce = Vector2.zero;
        }
    }

    public void addTorque(Vector2 force)
    {
        _angularForce = force;
    }
    #endregion

    #region(LinearVelocity)

    private void LinearMovement()
    {
        Vector2 a = _force / _rb.mass;
        _force = Vector2.zero;

        _rb.velocity += finalVelocity(a);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
        _maxSpeed = Mathf.Max(10, Mathf.Min(_rb.velocity.magnitude, _maxSpeed));
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
    #endregion

    public bool linearDependency(Vector2 a, Vector2 b)
    {
        return (a.x * b.x) + (a.y * b.y) != 0;
    }
}
