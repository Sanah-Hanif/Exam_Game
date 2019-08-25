using Interactions;
using ScriptableObjects.Interactions;
using ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Player
{
    public class PlayerInteraction : InteractionController
    {

        [SerializeField] private ScriptableObjects.Interactions.Interaction Interaction;
        [SerializeField] private PlayerSettings Settings;
        [SerializeField] private PlayerInput input;

        private InputActionMap ability;
        private InputActionMap movement;
        private GameObject CreatedObject;
        
        private ReadOnlyArray<InputActionMap> Active;

        private void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            if(CreatedObject)
                Move();
        }

        private void Initialize()
        {
            Active = input.actions.actionMaps;
            ability = Active[Active.Count-1];
            movement = Active[0];
            ability.Disable();
            movement.GetAction("Ability").performed += ctx => Interact();
            ability.GetAction("Rotate").performed += ctx => Rotate(ctx.ReadValue<float>());
            ability.GetAction("Place").performed += ctx => Place();
            ability.GetAction("Cancel").performed += ctx => Cancel();
        }
        
        public override void Interact()
        {
            Interaction.Interact(transform.position);
            var create = (CreationInteraction)Interaction;
            CreatedObject = create.createdObject;
            var rb = CreatedObject.GetComponent<Rigidbody2D>();
            if (rb)
                rb.gravityScale = 0;
            InitMovingObject();
        }

        private void InitMovingObject()
        {
            ability.Enable();
        }
        
        private void Cancel()
        {
            ability.Disable();
            Destroy(CreatedObject);
            CreatedObject = null;
            Interaction.Interact(transform.position);
            var create = (CreationInteraction)Interaction;
            create.createdObject = null;
        }

        private void Place()
        {
            ability.Disable();
            if(CreatedObject.GetComponent<Rigidbody2D>())
                CreatedObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            if(CreatedObject.GetComponent<InteractionController>())
                CreatedObject.GetComponent<InteractionController>().Interact();
            CreatedObject = null;
        }

        private void Rotate(float value)
        {
            CreatedObject.transform.Rotate(0,0, 90f * value);
        }

        private void Move()
        {
            Vector2 move = ability.GetAction("Movement").ReadValue<Vector2>() * 0.1f;
            Vector2 oldPos = CreatedObject.transform.position;
            oldPos += move;
            Vector2 moveAmmount = oldPos - (Vector2) transform.position;
            Vector2 clamped = Vector2.ClampMagnitude(moveAmmount, Settings.interactionSpawnRadius);
            Vector2 newPos = (Vector2) transform.position + clamped;
            CreatedObject.transform.position = newPos;
        }

        private void OnDisable()
        {
            movement.GetAction("Ability").performed -= ctx => Interact();
            ability.GetAction("Rotate").performed -= ctx => Rotate(ctx.ReadValue<float>());
            ability.GetAction("Place").performed -= ctx => Place();
            ability.GetAction("Cancel").performed -= ctx => Cancel();
            ability.Disable();
        }
    }
}

