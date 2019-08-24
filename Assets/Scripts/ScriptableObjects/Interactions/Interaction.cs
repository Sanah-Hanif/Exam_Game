using UnityEngine;

namespace ScriptableObjects
{
    public abstract class Interaction : ScriptableObject
    {
        public virtual void Interact(Vector2 _position){}
    }
}
