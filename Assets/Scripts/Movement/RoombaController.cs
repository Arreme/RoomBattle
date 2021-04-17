using UnityEngine;

public class RoombaController : MonoBehaviour
{
    private Vector2 _vector = Vector2.zero;
    private Rigidbody2D _rb;
    private CustomPhysics _phy;
    [SerializeField] private InputManager _input;


    [Header("Movement variables")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] float _lerp = 0f;
    [SerializeField] float _maxVel = 0f;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _phy = gameObject.GetComponent<CustomPhysics>();
    }

    void FixedUpdate()
    {
        _vector = _input.Player1;
        _rb.AddForce(_vector*_speed*2,ForceMode2D.Force);
        float distance = Mathf.Sqrt((_rb.velocity.x * _rb.velocity.x) + (_rb.velocity.y * _rb.velocity.y));
        //Debug.Log(distance);
        Vector2 roomvalocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -_maxVel, _maxVel), Mathf.Clamp(_rb.velocity.y, -_maxVel, _maxVel));
        _rb.velocity = roomvalocity;
        
        transform.up = Vector2.Lerp(_vector,transform.up, _lerp);
    }

    //public void OnMoveEvent(Vector2 vector)
    //{
    //    float dotProd = (_vector.x * transform.up.x) + (_vector.y * transform.up.y);
    //    if ( dotProd >= -0.2 && dotProd <= 0.2)
    //    {
    //        _rb.rotation += 1f;
    //        Debug.Log("LinealDependency");
    //    }
    //    _vector = vector;
    //}
}
