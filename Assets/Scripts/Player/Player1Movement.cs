using System;
using UnityEngine;

namespace Player
{
    public class Player1Movement : PlayerMovement
    {

        private void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            MoveLeftStick();
            FallDown();
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            Control.Player1.Enable();
            Control.Player1.Jump.performed += ctx => Jump();
            Control.Player1.JumpHold.performed += ctx => JumpHold();
        }
        
        protected override void MoveLeftStick()
        {
            var direc = Control.Player1.Move.ReadValue<Vector2>();
            var velocity = rigidBody.velocity;
            if (direc.magnitude > 0.5f)
            {
                velocity.x = settings.maxSpeed * direc.x;
                rigidBody.velocity = velocity;
            }
            else
            {
                velocity = rigidBody.velocity;
                velocity.x = 0;
                rigidBody.velocity = velocity;
            }
        }

        protected override void Jump()
        {
            if (!IsGrounded)
                return;
            IsGrounded = false;
            Debug.Log("Jump", gameObject);
            Vector2 velocity = rigidBody.velocity;
            velocity.y = settings.jumpVelocity;
            rigidBody.velocity = velocity;
        }
        
        private void JumpHold()
        {
            if (DoubleJumped)
                return;
            DoubleJumped = true;
            Debug.Log("JumpHold", gameObject);
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
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                DoubleJumped = false;
                IsGrounded = true;
            }
        }

        public override void Disable()
        {
            Control.Player1.Disable();
        }

        public override void Enable()
        {
            Initialize();
        }
    }
}
