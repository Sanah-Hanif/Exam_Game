using UnityEngine;

namespace ScriptableObjects.Interactions
{
    [CreateAssetMenu(menuName = "Interaction/Creation Interaction", fileName = "new Creation Interaction")]
    public class CreationInteraction : Interaction
    {

        public GameObject createdObject;
        
        [SerializeField] private GameObject creation;
        
        public override void Interact(Vector2 _position)
        {
            if(!createdObject)
                createdObject = Instantiate(creation, _position, Quaternion.identity);
            else
            {
                createdObject.transform.position = _position;
            }
        }
    }
}
