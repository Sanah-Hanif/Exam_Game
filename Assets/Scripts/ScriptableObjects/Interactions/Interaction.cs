using UnityEngine;

namespace ScriptableObjects.Interactions
{
    
    public abstract class Interaction : ScriptableObject
    {
        public virtual void Interact(Vector2 _position){}
    }
}
