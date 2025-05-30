using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static event Action<Vector2> OnPlayerMovement;

    public static event Action<Vector2> OnMovementCamera;
    public static event Action OnInteractue;
    public static event Action OnChangeBarTime;
    public static event Action shoot;
    public static event Action<bool> shoot2;
    public static event Action OnstopGame;


    public void Movement(InputAction.CallbackContext context)
    {
        OnPlayerMovement?.Invoke(context.ReadValue<Vector2>());
    }


    public void MovementCamera(InputAction.CallbackContext context)
    {
        OnMovementCamera?.Invoke(context.ReadValue<Vector2>());
    }
    public void Interactue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractue?.Invoke();
        }
    }
    public void PressCtrl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnChangeBarTime?.Invoke();
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("a");
        if (context.started)
        {
            shoot?.Invoke();
        }
        shoot2?.Invoke(context.performed);
    }

    public void PressEsc(InputAction.CallbackContext context)
    {
        if (InputActionPhase.Performed != context.phase) return;
        OnstopGame?.Invoke();
    }

}
