using System;
using Interaction;
using Interactions;
using ScriptableObjects;
using ScriptableObjects.Interactions;
using ScriptableObjects.Player;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : InteractionController
    {

        [SerializeField] private ScriptableObjects.Interaction Interaction;
        [SerializeField] private PlayerSettings Settings;

        private PlayerControls Controls;
        private GameObject CreatedObject;

        private void Awake()
        {
            Controls = new PlayerControls();
            Initialize();
        }

        private void FixedUpdate()
        {
            if(CreatedObject)
                Move();
        }

        private void Initialize()
        {
            Controls.Player1.Enable();
            Controls.Player1.Ability.performed += ctx => Interact();
            Controls.Ability.Rotate.performed += ctx => Rotate(ctx.ReadValue<float>());
            Controls.Ability.Place.performed += ctx => Place();
            Controls.Ability.Cancel.performed += ctx => Cancel();
        }
        
        public override void Interact()
        {
            Interaction.Interact(transform.position);
            var create = (CreationInteraction)Interaction;
            CreatedObject = create.createdObject;
            InitMovingObject();
        }

        void InitMovingObject()
        {
            Controls.Ability.Enable();
            Controls.Player1.Disable();
        }
        
        private void Cancel()
        {
            Controls.Ability.Disable();
            Destroy(CreatedObject);
            CreatedObject = null;
        }

        private void Place()
        {
            Controls.Ability.Disable();
            Controls.Player1.Enable();
            CreatedObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            CreatedObject = null;
        }

        private void Rotate(float value)
        {
            CreatedObject.transform.Rotate(0,0, 90f * value);
        }

        void Move()
        {
            Vector2 move = Controls.Ability.Movement.ReadValue<Vector2>() * 0.1f;
            Vector2 oldPos = CreatedObject.transform.position;
            oldPos += move;
            Vector2 moveAmmount = oldPos - (Vector2) transform.position;
            Vector2 clamped = Vector2.ClampMagnitude(moveAmmount, Settings.interactionSpawnRadius);
            Vector2 newPos = (Vector2) transform.position + clamped;
            CreatedObject.transform.position = newPos;
        }
    }
}
