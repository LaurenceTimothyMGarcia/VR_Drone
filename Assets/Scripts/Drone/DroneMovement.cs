using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInput
{

    public class DroneMovement : MonoBehaviour
    {

        // Drones move on 4 different controls
        // Roll - Moving Left/Right Horizontally
        //      - Done with horizontal movement on Right Stick
        // Pitch - Moving Forward/Backward Horizontally
        //      - Done with vertical movement on Right Stick
        // Yaw  - Rotates Drone Left or Right
        //      - Done with horizontal movement on Left Stick
        // Throttle - Controls power of propellors moving it vertically
        //      - Done with vertical movement of Left Stick

        // Variables editable in the inspector
        [Header("Drone Control Speeds")]

        [Tooltip("Drone Movement Left/Right controlled using the Right Stick")]
        [SerializeField] private float rollSpeed;

        [Tooltip("Drone Movement Forward/Backward controlled using the Right Stick")]
        [SerializeField] private float pitchSpeed;

        [Tooltip("Drone Rotation Left/Right controlled using the Left Stick")]
        [SerializeField] private float yawSpeed;

        [Tooltip("Drone Movement Up/Down controlled using the Left Stick")]
        [SerializeField] private float throttlePower;


        // Variables of current status of drone
        private float currentRollSpeed;
        private float currentPitchSpeed;
        private float currentYawSpeed;
        private float currentThrottlePower;

        // Inputs
        private float rollInput;
        private float pitchInput;
        private float yawInput;
        private float throttleInput;


        private Rigidbody rb;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            Input();

            SpeedControl();
        }

        // Physics related calculations here
        private void FixedUpdate()
        {
            MoveDrone();
        }

        private void Input()
        {
            rollInput = DroneInputManager.Instance.rollInput;
            pitchInput = DroneInputManager.Instance.pitchInput;
            yawInput = DroneInputManager.Instance.yawInput;
            throttleInput = DroneInputManager.Instance.throttleInput;
        }


        private void MoveDrone()
        {
            Vector3 movement = new Vector3((rollInput * rollSpeed), (throttleInput * throttlePower), (pitchInput * pitchSpeed));
            rb.AddForce(movement, ForceMode.Force);
        }

        private void SpeedControl()
        {
            float limitRoll = rb.velocity.x;
            float limitThrottle = rb.velocity.y;
            float limitPitch = rb.velocity.z;

            Vector3 flatVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

            if (flatVel.magnitude > rollSpeed)
            {
                Vector3 limitVel = flatVel.normalized * rollSpeed;
                limitRoll = limitVel.x;
            }

            if (flatVel.magnitude > throttlePower)
            {
                Vector3 limitVel = flatVel.normalized * throttlePower;
                limitThrottle = limitVel.y;
            }

            if (flatVel.magnitude > pitchSpeed)
            {
                Vector3 limitVel = flatVel.normalized * pitchSpeed;
                limitPitch = limitVel.z;
            }

        }
    }
}

