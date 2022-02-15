using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class Interactable : InteractableBase
    {
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            gameObject.SetActive(false);
        }
    }

    
}