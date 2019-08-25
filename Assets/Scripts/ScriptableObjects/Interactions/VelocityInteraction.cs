using UnityEngine;

namespace ScriptableObjects.Interactions
{
    [CreateAssetMenu(menuName = "Interaction/Velocity Interaction", fileName = "new Velocity Interaction")]
    public class VelocityInteraction : Interaction
    {

        [SerializeField] private float velocity = 3f;
    
        public override void Interact(Vector2 _position)
        {
            
        }
    }
}
