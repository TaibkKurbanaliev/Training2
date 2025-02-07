using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private bool _holdToSprting = true;

    public InputSystem_Actions InputActions { get; private set; }

    public Vector2 MovementInput {  get; private set; }
    public Vector2 MouseInput { get; private set; }
    public bool IsSprinting { get;private set; }

    private void OnEnable()
    {
        InputActions = new InputSystem_Actions();
        InputActions.Enable();

        InputActions.Player.Enable();
        InputActions.Player.SetCallbacks(this);
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
        InputActions.Player.RemoveCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSprinting = _holdToSprting || !IsSprinting;
        }
        else if (context.canceled) 
        {
            IsSprinting = !_holdToSprting && IsSprinting;
        }
    }
}
