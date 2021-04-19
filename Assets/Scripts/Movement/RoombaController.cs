using UnityEngine;

public class RoombaController : MonoBehaviour
{
    private Vector2 _vector = Vector2.zero;
    private CustomPhysics _phy;
    [SerializeField] private InputManager _input;

    public enum Players { Player1, Player2, Player3, Player4 }
    public Players players;

    [SerializeField] bool _boosting = false;
    private float _currentSpeed;

    [Header("Movement variables")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] float _maxVel = 10f;
    [SerializeField] float _linearDrag = 1.4f;
    [SerializeField] float _angularDrag = 0.05f;
    [SerializeField] float _rotateSpeed = 2.5f;
    
    [Header("Boosting variables")]
    [SerializeField] private float _boostMaxSpeed = 40f;
    [SerializeField] private float _boostIncrement = 1.15f;

    private void Start()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();
        _phy.MaxSpeed = _maxVel;
        _phy.LinearDrag = _linearDrag;
        _phy.AngularDrag = _angularDrag;
        switch(players)
        {
            case Players.Player1:
                _input.P1Boost += Boost;
                break;
            case Players.Player2:
                _input.P2Boost += Boost;
                break;
            default:
                break;
        }
        
    }

    void FixedUpdate()
    {
        switch (players)
        {
            case Players.Player1:
                _vector = _input.Player1;
                break;
            case Players.Player2:
                _vector = _input.Player2;
                break;
            default:
                break;
        }

        Debug.DrawLine(transform.position + Vector3.zero, transform.position + transform.up, Color.red);
        Debug.DrawLine(transform.position + Vector3.zero, transform.position +  new Vector3(_vector.x, _vector.y, 0), Color.green);
        if (_boosting)
        {
            _phy.MaxSpeed = _boostMaxSpeed;
            _currentSpeed = Mathf.Min(_boostMaxSpeed, _currentSpeed * _boostIncrement);
            if (_vector != Vector2.zero)
            {
                _phy.addTorque(bisector(_vector, transform.up) * _rotateSpeed, 1);
            }
        }
        else
        {
            _currentSpeed = _speed;
            if (_vector != Vector2.zero)
            {
                _phy.addTorque(bisector(_vector,transform.up)*_rotateSpeed, 1);
            }
            
        }
        moveRoomba(_currentSpeed);
    }

    private void moveRoomba(float currentSpeed)
    {
        _phy.addForce(_vector, currentSpeed);
    }
    private void Boost()
    {
        _boosting = !_boosting;
        if (_boosting) _currentSpeed = _speed;
    }

    #region(UtilityFunctions)
    private Vector2 bisector(Vector2 a, Vector2 b)
    {
        if (_phy.linearDependency(a,b))
        {
            a += new Vector2(0.0001f,0.0001f);
        }
        return (b.magnitude * a + a.magnitude * b).normalized;
    }
    #endregion
}
