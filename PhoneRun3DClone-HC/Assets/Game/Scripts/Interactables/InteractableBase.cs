using System;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected InteractableType baseInteractableType;

        public virtual void Move()
        {
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InteractableManager.Instance.SetInteractableValue(baseInteractableType);
            }
        }
    }
    
}

