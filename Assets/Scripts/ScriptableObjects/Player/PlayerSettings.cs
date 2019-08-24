using UnityEngine;

namespace ScriptableObjects.Player
{
    [CreateAssetMenu(menuName = "Player Settings", fileName = "New Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement Parameters")]
        public float maxSpeed = 2f;
        public float acceleration = 1f;
        
        [Header("Jump Parameters")]
        public float jumpVelocity = 2f;
        public float jumpHoldMultiplier = 2f;
        
        [Header("Falling Parameters")]
        public float fallDownThreshHold = 1f;
        public float fallSpeedIncrease = 0.1f;

        [Header("Interaction Spawn Radius")] 
        public float interactionSpawnRadius = 3f;
    }
}
