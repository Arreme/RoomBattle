using UnityEngine;

public class RoombaController : MonoBehaviour
{
    public CustomPhysics _phy;
    [SerializeField] private InputManager _input;

    private RoombaState _currentState;

    public BoostingState _boostingState = new BoostingState();
    public NormalState _normalState = new NormalState();

    public enum Players { Player1, Player2, Player3, Player4 }
    public Players players;

    [Header("Movement variables")]
    public Vector3 _inputVector = Vector3.zero;
    [SerializeField] private float _speed = 10f;
    [SerializeField] float _maxVel = 10f;
    [SerializeField] float _rotateSpeed = 2.5f;

    [Header("Boosting variables")]
    public bool _boosting = false;
    [SerializeField] float _maxFuel = 2f;
    [SerializeField] float _boostFuel = 2f;
    [SerializeField] private float _boostMaxSpeed = 40f;
    [SerializeField] private float _boostIncrement = 1.10f;

    private void Start()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();

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
        _currentState = _normalState;
        
    }

    void FixedUpdate()
    {
        _currentState = _currentState.runFrame(this);
    }

    private void ChooseVectorPlayer()
    {
        switch (players)
        {
            case Players.Player1:
                _inputVector = _input.Player1;
                break;
            case Players.Player2:
                _inputVector = _input.Player2;
                break;
            default:
                break;
        }
    }

    private void moveRoomba(float currentSpeed)
    {
        _phy.addForce(_inputVector, currentSpeed);
    }
    private void Boost()
    {
        _boosting = !_boosting;
    }
}
