using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneController : MonoBehaviour
{
    //Values of input
    private Vector2 rightStick;
    private Vector2 leftStick;

    //Booleans to check if the joystick was moved or not
    [HideInInspector] public bool rollCheck;
    [HideInInspector] public bool pitchCheck;
    [HideInInspector] public bool throttleCheck;
    [HideInInspector] public bool yawCheck;

    //Values of control based on input
    [HideInInspector] public float rollInput;
    [HideInInspector] public float pitchInput;
    [HideInInspector] public float throttleInput;
    [HideInInspector] public float yawInput;

    // Start is called before the first frame update
    // Initialize the input drone controls
    void Start()
    {
        InputManager.Instance.playerActions.DroneControls.VerticalMovementRotation.performed += OnLeftStick;
        InputManager.Instance.playerActions.DroneControls.VerticalMovementRotation.canceled += OnLeftStick;
        InputManager.Instance.playerActions.DroneControls.HorizontalMovement.performed += OnRightStick;
        InputManager.Instance.playerActions.DroneControls.HorizontalMovement.canceled += OnRightStick;
    }

    public void OnRightStick(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            rightStick = value.ReadValue<Vector2>();

            rollCheck = rightStick.x != 0;
            pitchCheck = rightStick.y != 0;

            if (rollCheck)
            {
                rollInput = rightStick.x;
            }

            if (pitchCheck)
            {

            }
        }
        else if (value.canceled)
        {
            //Set velocity to zero
        }
    }

    public void OnLeftStick(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            leftStick = value.ReadValue<Vector2>();

            yawCheck = leftStick.x != 0;
            throttleCheck = leftStick.y != 0;

            if (yawCheck)
            {
                yawInput = leftStick.x;
            }

            if (throttleCheck)
            {
                throttleInput = leftStick.y;
            }
        }
        else if (value.canceled)
        {
            //Set velocity to zero
        }
    }

}
