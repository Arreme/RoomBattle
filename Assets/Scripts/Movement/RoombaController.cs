using UnityEngine;

public class RoombaController : MonoBehaviour
{
    private Vector2 _vector = Vector2.zero;
    private CustomPhysics _phy;
    [SerializeField] private InputManager _input;

    public enum Players { Player1, Player2, Player3, Player4 }
    public Players players;

    [Header("Movement variables")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] float _lerp = 0.7f;
    [SerializeField] float _maxVel = 10f;
    [SerializeField] float _linearDrag = 1.4f;
    [SerializeField] bool _boosting = false;

    private void Awake()
    {
        _phy = gameObject.GetComponent<CustomPhysics>();
        _phy.MaxSpeed = _maxVel;
        _phy.LinearDrag = _linearDrag;
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

        if (_boosting)
        {
            _phy.MaxSpeed = _maxVel * 4f;
            _speed = Mathf.Min(_maxVel*10,_speed*1.2f);
            transform.up = Vector2.Lerp(bisector(_vector, transform.up), transform.up, _lerp / 1.2f);
            _phy.addForce(bisector(_vector, transform.up), _speed);
            
        } else
        {
            _speed = 15f;
            _phy.addForce(_vector, _speed);
            transform.up = Vector2.Lerp(bisector(_vector, transform.up), transform.up, _lerp);
        }
    }

    private void Boost()
    {
        _boosting = !_boosting;
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
