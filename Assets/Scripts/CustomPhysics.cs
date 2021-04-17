using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Determina la següent posició del Roomba o d'un objecte amb una massa
 * determinada
 */
public class CustomPhysics : MonoBehaviour
{
    [SerializeField] private float mass;
    private Vector2 velocity = Vector2.zero;
    private Vector2 position = Vector2.zero;
    private Vector2 force = Vector2.zero;

    private Transform _tr;
    private void Awake()
    {
        _tr = gameObject.GetComponent<Transform>();
    }

    public void addForce(Vector2 force)
    {
        //F = m * a, a = F/m
        Vector2 a = force / mass;
        velocity += finalVelocity(a);
        position += finalPosition(velocity);
        _tr.position = position;
    }

    public Vector2 finalVelocity(Vector2 a)
    {
        //vel = v0 + a*t
        return velocity + a*Time.deltaTime;
    }

    public Vector2 finalPosition(Vector2 v)
    {
        //x = x0 + v*t
        return position + v * Time.deltaTime;
    }

    public float vectorDistance(Vector2 a)
    {
        return Mathf.Sqrt((a.x * a.x) + (a.y * a.y));
    }

    public bool linearDependency(Vector2 a, Vector2 b)
    {
        return (a.x * b.x) + (a.y * b.y) == 0;
    }
}
