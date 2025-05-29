using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static event Action<Vector2> OnPlayerMovement;

    public static event Action<Vector2> OnMovementCamera;
    public static event Action interactive;


    public void Movement(InputAction.CallbackContext context)
    {
        OnPlayerMovement?.Invoke(context.ReadValue<Vector2>());
    }


    public void MovementCamera(InputAction.CallbackContext context)
    {
        OnMovementCamera?.Invoke(context.ReadValue<Vector2>());
    }
}
