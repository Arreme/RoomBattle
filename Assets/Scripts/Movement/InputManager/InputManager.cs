using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    RoombaImputSystem _input;
    private Vector2 _player1;
    public Vector2 Player1
    {
        get { return _player1; }
    }

    private Vector2 _player2;
    public Vector2 Player2
    {
        get { return _player2; }
    }

    void Awake()
    {
        _input = new RoombaImputSystem();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player1.Move.performed += P1MovePerformed;
        _input.Player1.Move.canceled += P1MovePerformed;
        _input.Player1.Boost.performed += P1BoostPerformed;
        _input.Player1.Boost.canceled += P1BoostPerformed;

        _input.Player2.Move.performed += P2MovePerformed;
        _input.Player2.Move.canceled += P2MovePerformed;
        _input.Player2.Boost.performed += P2BoostPerformed;
        _input.Player2.Boost.canceled += P2BoostPerformed;
    }

    private void P1MovePerformed(InputAction.CallbackContext ctx)
    {
        _player1 = ctx.ReadValue<Vector2>();
    }

    public delegate void PlayerBoost();
    //event  
    public event PlayerBoost P1Boost;

    private void P1BoostPerformed(InputAction.CallbackContext ctx)
    {
        P1Boost();
    }

    private void P2MovePerformed(InputAction.CallbackContext ctx)
    {
        _player2 = ctx.ReadValue<Vector2>();
    }
    //event  
    public event PlayerBoost P2Boost;

    private void P2BoostPerformed(InputAction.CallbackContext ctx)
    {
        P2Boost();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
