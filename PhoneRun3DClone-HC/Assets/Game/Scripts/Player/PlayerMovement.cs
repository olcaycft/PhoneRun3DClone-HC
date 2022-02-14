using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform cannonRightLimit;
    [SerializeField] private Transform cannonLeftLimit;
    private float cannonRightLimitX => cannonRightLimit.localPosition.x;
    private float cannonLeftLimitX => cannonLeftLimit.localPosition.x;

    //private float sideMovementSensitivity => SettingsManager.GameSettings.playerSideMovementSensitivity;
    //private float sideMovementLerpSpeed => SettingsManager.GameSettings.playerSideMovementLerpSpeed;
    //private float forwardSpeed => SettingsManager.GameSettings.playerForwardSpeed;
    
    private float sideMovementSensitivity = 20f;
    private float sideMovementLerpSpeed = 5f;
    private float forwardSpeed = 5f;

    private float sideMovementTarget = 0f;

    private Vector2 mousePositionCM
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimeters = inches * 2.54f;

            return centimeters;
        }
    }

    private void Update()
    {
        HandleInput();
        SideMovement();
        ForwardMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = mousePositionCM;
        }

        if (Input.GetMouseButton(0))
        {
            var deltaMouse = mousePositionCM - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = mousePositionCM;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    private void SideMovement()
    {
        sideMovementTarget += inputDrag.x * sideMovementSensitivity;
        sideMovementTarget = Mathf.Clamp(sideMovementTarget, cannonLeftLimitX, cannonRightLimitX);
        var localPos = sideMovementRoot.localPosition;
        localPos.x = Mathf.Lerp(localPos.x, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);
        sideMovementRoot.localPosition = localPos;
    }

    private void ForwardMovement()
    {
        transform.position+=Vector3.forward*Time.deltaTime* forwardSpeed;
    }
}