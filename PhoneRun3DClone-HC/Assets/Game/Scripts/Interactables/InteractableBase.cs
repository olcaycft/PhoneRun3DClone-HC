using System;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] private InteractableType interactable;

        public virtual void Move()
        {
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InteractableManager.Instance.SetInteractableValue(interactable);
            }
        }
    }
    
    public enum InteractableType
    {
        Collectable3,
        Collectable10,
        Collectable15,
        Obstacle20,
        ObstacleHacker,
        GoodGate50,
        GoodGate100,
        BadGate50,
        BadGate100
    }
}

