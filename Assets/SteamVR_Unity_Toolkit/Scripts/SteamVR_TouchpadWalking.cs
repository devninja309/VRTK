﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteamVR_TouchpadWalking : MonoBehaviour {
    public float maxWalkSpeed = 3f;
    public float deceleration = 0.1f;

    private Vector2 touchAxis;
    private float movementSpeed = 0f;
    private float strafeSpeed = 0f;

    private SteamVR_PlayerPresence playerPresence;
    private SteamVR_TrackedObject trackedController;
    private List<uint> trackedControllerIndices;

    private void Awake()
    {
        if (this.GetComponent<SteamVR_PlayerPresence>())
        {
            playerPresence = this.GetComponent<SteamVR_PlayerPresence>();
        }
        else
        {
            Debug.LogError("The SteamVR_TouchpadWalking script requires the SteamVR_PlayerPresence script to be attached to the [CameraRig]");
        }
    }

    private void Start () {
        this.name = "PlayerObject_" + this.name;
        trackedControllerIndices = new List<uint>();
        SteamVR_Utils.Event.Listen("device_connected", OnDeviceConnected);
    }

    private void DoTouchpadAxisChanged(object sender, ControllerClickedEventArgs e)
    {
        touchAxis = e.touchpadAxis;
    }

    private void DoTouchpadUntouched(object sender, ControllerClickedEventArgs e)
    {
        touchAxis = Vector2.zero;
    }

    private void CalculateSpeed(ref float speed, float inputValue)
    {
        if (inputValue != 0f)
        {
            speed = (maxWalkSpeed * inputValue);
        }
        else
        {
            Decelerate(ref speed);
        }
    }

    private void Decelerate(ref float speed)
    {
        if (speed > 0)
        {
            speed -= Mathf.Lerp(deceleration, maxWalkSpeed, 0f);
        }
        else if (speed < 0)
        {
            speed += Mathf.Lerp(deceleration, -maxWalkSpeed, 0f);
        }
        else
        {
            speed = 0;
        }

        float deadzone = 0.1f;
        if (speed < deadzone && speed > -deadzone)
        {
            speed = 0;
        }
    }

    private void Move()
    {
        var movement = playerPresence.GetHeadset().forward * movementSpeed * Time.deltaTime;
        var strafe = playerPresence.GetHeadset().right * strafeSpeed * Time.deltaTime;
        float fixY = this.transform.position.y;
        this.transform.position += (movement + strafe);
        this.transform.position = new Vector3(this.transform.position.x, fixY, this.transform.position.z);
    }

    private void FixedUpdate()
    {
        CalculateSpeed(ref movementSpeed, touchAxis.y);
        CalculateSpeed(ref strafeSpeed, touchAxis.x);
        Move();
    }

    private void OnDeviceConnected(params object[] args)
    {
        StartCoroutine(InitListeners((uint)(int)args[0], (bool)args[1]));
    }

    IEnumerator InitListeners(uint trackedControllerIndex, bool trackedControllerConnectedState)
    {
        trackedController = DeviceFinder.ControllerByIndex(trackedControllerIndex);
        var tries = 0f;
        while (!trackedController && tries < DeviceFinder.initTries)
        {
            tries+= Time.deltaTime;
            trackedController = DeviceFinder.ControllerByIndex(trackedControllerIndex);
            yield return null;
        }

        if (trackedController)
        {
            var controllerEvent = trackedController.GetComponent<SteamVR_ControllerEvents>();
            if (controllerEvent)
            {
                if (trackedControllerConnectedState && !trackedControllerIndices.Contains(trackedControllerIndex))
                {
                    controllerEvent.TouchpadAxisChanged += new ControllerClickedEventHandler(DoTouchpadAxisChanged);
                    controllerEvent.TouchpadUntouched += new ControllerClickedEventHandler(DoTouchpadUntouched);
                    trackedControllerIndices.Add(trackedControllerIndex);
                }
            }
        }
    }
}