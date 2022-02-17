using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform playerRightLimit;
    [SerializeField] private Transform playerLeftLimit;
    private float playerRightLimitX => playerRightLimit.localPosition.x;
    private float playerLeftLimitX => playerLeftLimit.localPosition.x;

    //private float sideMovementSensitivity => SettingsManager.GameSettings.playerSideMovementSensitivity;
    //private float sideMovementLerpSpeed => SettingsManager.GameSettings.playerSideMovementLerpSpeed;
    //private float forwardSpeed => SettingsManager.GameSettings.playerForwardSpeed;

    private float sideMovementSensitivity = 1f;
    private float sideMovementLerpSpeed = 5f;
    private float forwardSpeed = 5f;

    private float sideMovementTarget = 0f;

    private bool isGameStart;
    private bool isLevelFinish;

    private void OnEnable()
    {
        GameManager.levelFinishedObserver += ChangeLevelState;
    }

    private void OnDestroy()
    {
        GameManager.levelFinishedObserver -= ChangeLevelState;
    }

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
        if (isLevelFinish)
        {
            return;
        }
        HandleInput();
        SideMovement();
        if (!isGameStart)
        {
            return;
        }

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

            if (isGameStart || (inputDrag.x == 0 && inputDrag.y == 0)) return;
            isGameStart = true;
            GameManager.Instance.StartThisLevel();
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }
    private void SideMovement()
    {
        sideMovementTarget += inputDrag.x * sideMovementSensitivity;
        sideMovementTarget = Mathf.Clamp(sideMovementTarget, playerLeftLimitX, playerRightLimitX);
        var localPos = sideMovementRoot.localPosition;
        localPos.x = Mathf.Lerp(localPos.x, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);
        sideMovementRoot.localPosition = localPos;
    }

    private void ForwardMovement()
    {
        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;
    }

    private void ChangeLevelState()
    {
        isLevelFinish = true;
    }
}