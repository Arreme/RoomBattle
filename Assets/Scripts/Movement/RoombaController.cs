using UnityEngine;

public class RoombaController : MonoBehaviour
{
    private Vector2 _vector = Vector2.zero;
    private CustomPhysics _phy;
    [SerializeField] private InputManager _input;


    [Header("Movement variables")]
    [SerializeField] private float _speed = 0f;
    [SerializeField] float _lerp = 0f;
    [SerializeField] float _maxVel = 0f;

    private void Awake()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();
        _phy.MaxSpeed = _maxVel;
    }

    void FixedUpdate()
    {
        _vector = _input.Player1;
        _phy.addForce(_vector , _speed);
        transform.up = Vector2.Lerp(bisector(_vector, transform.up), transform.up, _lerp);
    }

    private Vector2 bisector(Vector2 a, Vector2 b)
    {
        if (_phy.linearDependency(a,b))
        {
            a += new Vector2(0.0001f,0.0001f);
        }
        return (b.magnitude * a + a.magnitude * b).normalized;
    }
}
