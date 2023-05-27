
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    CustomInput controls;

    [HideInInspector] public Vector2 moveVector;
    [HideInInspector] public Vector2 lookVector;
    [HideInInspector] public float upDownValue;
    [HideInInspector] public bool jumping;
    [HideInInspector] public bool climbing;

    private void Awake()
    {
        if (controls == null) controls = new CustomInput();

        controls.Player.Jump.performed += OnJump;
        controls.Player.Climb.performed += OnClimb;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    public void OnDisable()
    {
        controls.Player.Disable();
    }

    public void Update()
    {
        moveVector = controls.Player.Move.ReadValue<Vector2>();
        lookVector = controls.Player.Look.ReadValue<Vector2>();
        upDownValue = controls.Player.UpDown.ReadValue<float>();
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started) jumping = true;
        else if (ctx.canceled) jumping = false;
    }

    private void OnClimb(InputAction.CallbackContext ctx)
    {
        if (ctx.started) climbing = true;
        else if (ctx.canceled) climbing = false;
    }
}
