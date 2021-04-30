
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerConfig playerConfig;
    private RoombaController player;

    [SerializeField]
    private MeshRenderer playerMesh;

    private RoombaImputSystem controls;
    
    void Awake()
    {
        player = GetComponent<RoombaController>();
        controls = new RoombaImputSystem();
    }

    public void InitializePlayer(PlayerConfig conf)
    {
        playerConfig = conf;
        playerMesh.material = playerConfig.PlayerMaterial;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.Player1.Move.name)
        {
            Debug.Log("Doing Something");
            MovePerformed(obj);
        } else if (obj.action.name == controls.Player1.Boost.name)
        {
            BoostPerformed(obj);
        } else
        {
            ActionPerformed(obj);
        }
    }

    public void MovePerformed(InputAction.CallbackContext ctx)
    {
        player.SetInputVector(ctx.ReadValue<Vector2>());
    }

    public void BoostPerformed(InputAction.CallbackContext ctx)
    {
        player.SetBoost(ctx.ReadValueAsButton());
    }

    public void ActionPerformed(InputAction.CallbackContext ctx)
    {
        player.SetAction(ctx.ReadValueAsButton());
    }
}
