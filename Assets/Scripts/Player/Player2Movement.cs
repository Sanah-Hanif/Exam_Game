using UnityEngine;

namespace Player
{
    public class Player2Movement : PlayerMovement
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
            Control.Player2.Enable();
            Control.Player2.Jump.performed += ctx => Jump();
            //Control.Player2.JumpHold.performed += ctx => JumpHold();
        }
        
        protected override void MoveLeftStick()
        {
            var direc = Control.Player2.Move.ReadValue<Vector2>();
            Debug.Log(direc);
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
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                DoubleJumped = false;
                IsGrounded = true;
            }
        }
    }
}
