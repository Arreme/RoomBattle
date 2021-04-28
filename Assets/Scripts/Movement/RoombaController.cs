using UnityEngine;

public class RoombaController : MonoBehaviour
{
    [Header("Input")]
    public Vector3 _inputVector = Vector3.zero;

    public CustomPhysics _phy;
    [Header("Movement variables")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxVel = 10f;
    [SerializeField] private float _rotateSpeed = 2.5f;

    [Header("Boosting variables")]
    public bool _boosting = false;
    [SerializeField] private float _boostTime = 2f;
    [SerializeField] private float _boostMaxSpeed = 40f;

    [Header("States")]
    public bool _powerUp = false;
    private RoombaState _currentState;

    public BoostingState _boostingState;
    public NormalState _normalState;

    public PowerUp _currentPowerUp;

    private void Start()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();
        _normalState = new NormalState(_speed, _maxVel, _rotateSpeed);
        _boostingState = new BoostingState(_boostTime,_boostMaxSpeed);
        _currentState = _normalState;
        
    }

    void FixedUpdate()
    {
        _currentState = _currentState.runFrame(this);
    }

    public void SetInputVector(Vector2 input)
    {
        _inputVector = input;
    }

    public void SetBoost(bool pressed)
    {
        _boosting = pressed;
    }

    public void SetAction(bool pressed)
    {
        _powerUp = pressed;
    }

    public void getPowerUp(PowerUp powerUp)
    {
        _currentPowerUp = powerUp;
    }
}
