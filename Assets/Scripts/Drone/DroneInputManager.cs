using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UserInput 
{
    public class DroneInputManager : MonoBehaviour
    {

        //Singleton system for Drone Input
        public static DroneInputManager Instance = null;

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


        private void Awake()
        {
            //Initialize singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
                Destroy(this.gameObject);
            }

            //When right stick moved
            rightStickAction.performed += ctx => {

                rightStick = ctx.ReadValue<Vector2>();

                rollCheck = rightStick.x != 0;
                pitchCheck = rightStick.y != 0;

                if (rollCheck)
                {
                    rollInput = rightStick.x;
                }

                if (pitchCheck)
                {
                    pitchInput = rightStick.y;
                }
            };
            //Stops when right stick released
            rightStickAction.canceled += ctx => rightStick = Vector2.zero;

            //When left stick moved
            leftStickAction.performed += ctx => {

                leftStick = ctx.ReadValue<Vector2>();

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
            };
            //Stops when left stick released
            leftStickAction.canceled += ctx => leftStick = Vector2.zero;
        }

        private void OnEnable()
        {
            rightStickAction.Enable();
            leftStickAction.Enable();
        }

        private void OnDisable()
        {
            rightStickAction.Disable();
            leftStickAction.Disable();
        }
    }
}

