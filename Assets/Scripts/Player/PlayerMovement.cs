using System;
using ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public abstract class PlayerMovement : MonoBehaviour
    {

        protected Gamepad gamepad;
        protected bool IsGrounded = true;
        protected bool DoubleJumped = false;
        protected PlayerControls Control; 

        [SerializeField] protected Rigidbody2D rigidBody;
        [SerializeField] protected int playerNumber;
        [SerializeField] protected PlayerSettings settings;

        protected InputActionMap Active;
        
        protected virtual void Initialize()
        {
            Control = new PlayerControls();
        }
        protected virtual void MoveLeftStick() { }
        
        protected virtual void Jump(){}
        
        public virtual void Disable(){}
        public virtual void Enable(){}
    }
}
