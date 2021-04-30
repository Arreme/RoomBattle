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
    [SerializeField] private float _invincibilityTime = 6f;
    [SerializeField] private float _runPUTime = 5f;
    [SerializeField] private float _runPUSpeedMultiplier = 2f;
    public bool _powerUp = false;
    private RoombaState _currentState;

    public BoostingState _boostingState;
    public NormalState _normalState;

    public RunPowerUpState _runPUState;

    public PowerUp _currentPowerUp;

    private int _nChildNoBalloons = 3;
    [SerializeField] private MeshRenderer[] _mesh;
    private SphereCollider _col;

    private void Start()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();
        _col = gameObject.GetComponent<SphereCollider>();
        _normalState = new NormalState(_speed, _maxVel, _rotateSpeed,_mesh);
        _boostingState = new BoostingState(_boostTime,_boostMaxSpeed,_mesh);
        _runPUState = new RunPowerUpState(_runPUTime, _speed *_runPUSpeedMultiplier, _rotateSpeed, _mesh);
        _currentState = _normalState;
    }

    void FixedUpdate()
    {
        _currentState = _currentState.runFrame(this);

        if (transform.childCount <= _nChildNoBalloons)
        {
            destroyPlayer();
        }
    }

    private void destroyPlayer()
    {
        Destroy(gameObject);
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
        Debug.Log("powerup pressed");
        _powerUp = pressed;
    }

    public void getPowerUp(PowerUp powerUp)
    {
        _currentPowerUp = powerUp;
    }

    public void activateInvincibility()
    {
        _col.enabled = false;
        _mesh[1].material.color = Color.red;
        Invoke("revertInvincibility",_invincibilityTime);
    }

    private void revertInvincibility()
    {
        _col.enabled = true;
        _mesh[1].material.color = Color.blue;
    }
}
