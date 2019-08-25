using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Movement : MonoBehaviour
{
    private Gamepad gamepad;
    private bool IsGrounded = true;
    private bool DoubleJumped = false;
    private bool Moved = false;
    private PlayerControls Control;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private int playerNumber;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private PlayerInput input;

    private ReadOnlyArray<InputActionMap> Active;

    private InputActionMap map;

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        MoveLeftStick();
        FallDown();
    }

    private void Initialize()
    {
        Active = input.actions.actionMaps;
        map = Active[0];
        map.Enable();
        map.GetAction("Jump").performed += ctx => Jump();
        map.GetAction("JumpHold").performed += ctx => JumpHold();
    }

    private void MoveLeftStick()
    {
        var direc =  map.GetAction("move").ReadValue<Vector2>();
        var velocity = rigidBody.velocity;
        if (direc.magnitude > 0.5f)
        {
            Moved = true;
            velocity.x = settings.maxSpeed * direc.x;
            rigidBody.velocity = velocity;
        }
        else if(Moved)
        {
            Moved = false;
            velocity = rigidBody.velocity;
            velocity.x = 0;
            rigidBody.velocity = velocity;
        }
    }

    private void Jump()
    {
        if (!IsGrounded)
            return;
        IsGrounded = false;
        //Debug.Log("Jump", gameObject);
        Vector2 velocity = rigidBody.velocity;
        velocity.y = settings.jumpVelocity;
        rigidBody.velocity = velocity;
    }

    private void JumpHold()
    {
        if (DoubleJumped)
            return;
        DoubleJumped = true;
        //Debug.Log("JumpHold", gameObject);
        Vector2 velocity = rigidBody.velocity;
        velocity.y = settings.jumpVelocity * settings.jumpHoldMultiplier;
        rigidBody.velocity = velocity;
    }

    private void FallDown()
    {
        Vector2 velocity = rigidBody.velocity;
        if (velocity.y < settings.fallDownThreshHold)
        {
            //Debug.Log("falling down");
            velocity.y -= settings.fallSpeedIncrease;
            rigidBody.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DoubleJumped = false;
            IsGrounded = true;
        }
    }

    private void OnDisable()
    {
        map.GetAction("Jump").performed -= ctx => Jump();
        map.GetAction("JumpHold").performed -= ctx => JumpHold();
        map.Disable();
    }
}