using ScriptableObjects.Player;
using UnityEngine;

namespace Interaction
{
    public class Controlable : MonoBehaviour
    {
        private PlayerControls Controls;

        public Transform ParentObjet { get; set; }
        [SerializeField] private float moveAmmount = 1f;
        private float distance = 3f;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Controls = new PlayerControls();
            Controls.Ability.Enable();
            Controls.Ability.Rotate.performed += ctx => Rotate(ctx.ReadValue<float>());
            Controls.Ability.Place.performed += ctx => Place();
            Controls.Ability.Cancel.performed += ctx => Cancel();
        }

        private void Cancel()
        {
            
        }

        private void Place()
        {
            Disable();
            Destroy(this);
        }

        private void Disable()
        {
            Controls.Ability.Cancel.performed -= ctx => Cancel();
            Controls.Ability.Rotate.performed -= ctx => Rotate(ctx.ReadValue<float>());
            Controls.Ability.Place.performed -= ctx => Place();
            Controls.Ability.Disable();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if(!ParentObjet)
                return;
            Vector2 move = Controls.Ability.Movement.ReadValue<Vector2>();
            var transform1 = transform;
            Vector2 oldPos = transform1.position;
            oldPos.x += move.x;
            oldPos.y += move.y;
            oldPos = Vector2.ClampMagnitude((Vector2)ParentObjet.transform.position - oldPos, distance);
            transform1.position = oldPos;
        }

        private void Rotate(float value)
        {
            Debug.Log(value);
        }
    }
}
