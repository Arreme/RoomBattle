using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable]
public class MoveInputEvent : UnityEvent<Vector2> { }

public class InputManager : MonoBehaviour
{
    RoombaImputSystem _input;
    public MoveInputEvent _moveInputEvent;

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
        Vector2 vector = ctx.ReadValue<Vector2>();
        _moveInputEvent.Invoke(vector);
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
