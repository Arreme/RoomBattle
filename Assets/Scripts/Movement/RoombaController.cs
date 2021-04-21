using UnityEngine;

public class RoombaController : MonoBehaviour
{
    private Vector3 _vector = Vector3.zero;
    private CustomPhysics _phy;
    [SerializeField] private InputManager _input;

    public enum Players { Player1, Player2, Player3, Player4 }
    public Players players;

    [SerializeField] bool _boosting = false;
    [SerializeField] float _maxFuel = 2f;
    [SerializeField] float _boostFuel = 2f;
    private float _currentSpeed;

    [Header("Movement variables")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] float _maxVel = 10f;
    [SerializeField] float _rotateSpeed = 2.5f;

    [Header("Boosting variables")]
    [SerializeField] private float _boostMaxSpeed = 40f;
    [SerializeField] private float _boostIncrement = 1.10f;

    private void Start()
    {
        _currentSpeed = _speed;
        _phy = gameObject.GetComponent<CustomPhysics>();
        _phy.MaxSpeed = _maxVel;
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
        ChooseVectorPlayer();

        Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
        
        if (_boosting && _boostFuel > 0)
        { 
            _phy.MaxSpeed = _boostMaxSpeed;
            _currentSpeed = Mathf.Min(_boostMaxSpeed, _currentSpeed * _boostIncrement);
            _phy.addTorque(bisector(_vector, forward) * _rotateSpeed);
            _boostFuel = Mathf.Max(_boostFuel - Time.deltaTime, 0);
        }
        else
        {
            _currentSpeed = _speed;
            _phy.addTorque(bisector(_vector, forward) * _rotateSpeed);
            _boostFuel = Mathf.Min(_boostFuel + Time.deltaTime, _maxFuel);
        }
        moveRoomba(_currentSpeed);

        if(transform.childCount <= 1)
        {
            Destroy(gameObject);
        }
    }

    private void ChooseVectorPlayer()
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
    }

    private void moveRoomba(float currentSpeed)
    {
        _phy.addForce(_vector, currentSpeed);
    }
    private void Boost()
    {
        _boosting = !_boosting;
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
