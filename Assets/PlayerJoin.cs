using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{

    [SerializeField] private PlayerInputManager _manager;
    private void Update()
    {
        if (Keyboard.current.enterKey.isPressed)
        {
            //_manager.JoinPlayer(0,-1,"Player1",InputDevice.all);
        }
    }
}
