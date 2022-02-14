using System;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private InteractableType interactable;

        public static event Action<InteractableType> colisionWithInteractable;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                colisionWithInteractable?.Invoke(interactable);
                gameObject.SetActive(false);
            }
        }
    }

    public enum InteractableType
    {
        Collectable3,
        Collectable10,
        Collectable15,
        Obstacle20,
        ObstacleHacker
    }
}