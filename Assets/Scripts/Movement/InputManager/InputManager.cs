using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    RoombaImputSystem _input;
    private Vector2 _player1;
    public Vector2 Player1
    {
        get { return _player1; }
    }

    void Awake()
    {
        _input = new RoombaImputSystem();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player1.Move.performed += MovePerformed;
        _input.Player1.Move.canceled += MovePerformed;
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        _player1 = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
