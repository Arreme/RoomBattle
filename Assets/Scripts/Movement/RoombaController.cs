using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour
{
    private Vector2 _vector;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] float lerp = 0f;
    [SerializeField] float maxVel = 0f;
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.AddForce(_vector*_speed,ForceMode2D.Force);
        float distance = Mathf.Sqrt((_rb.velocity.x * _rb.velocity.x) + (_rb.velocity.y * _rb.velocity.y));
        //Debug.Log(distance);
        Vector2 roomvalocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -maxVel, maxVel), Mathf.Clamp(_rb.velocity.y, -maxVel, maxVel));
        _rb.velocity = roomvalocity;
        
        transform.up = Vector2.Lerp(_vector,transform.up, lerp);
    }

    public void OnMoveEvent(Vector2 vector)
    {
        float dotProd = (_vector.x * transform.up.x) + (_vector.y * transform.up.y);
        if ( dotProd >= -0.2 && dotProd <= 0.2)
        {
            _rb.rotation += 1f;
            Debug.Log("LinealDependency");
        }
        _vector = vector;
    }
}
