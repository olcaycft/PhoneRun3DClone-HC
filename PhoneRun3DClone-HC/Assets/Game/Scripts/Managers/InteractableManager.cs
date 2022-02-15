using System;
using Game.Scripts.Interactables;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class InteractableManager : MonoSingleton<InteractableManager>
    {
        private float playerSwapScore;

        public static event Action<float> interactableValueObserver;
        
        public void SetInteractableValue(InteractableType state)
        {
            var interactableValue = 0;
            switch (state)
            {
                case InteractableType.Collectable3:
                    interactableValue = 80;
                    break;
                case InteractableType.Collectable10:
                    interactableValue = 10;
                    break;
                case InteractableType.Collectable15:
                    interactableValue = 15;
                    break;
                case InteractableType.Obstacle20:
                    interactableValue = -20;
                    break;
                case InteractableType.Obstacle50:
                    interactableValue = -40;
                    break;
                case InteractableType.ObstacleOldLady:
                    interactableValue = -50;
                    break;
                case InteractableType.CollectableYoungLady:
                    interactableValue = 50;
                    break;
                case InteractableType.GoodGate50:
                    interactableValue = 50;
                    break;
                case InteractableType.GoodGate100:
                    interactableValue = 100;
                    break;
                case InteractableType.BadGate50:
                    interactableValue = -50;
                    break;
                case InteractableType.BadGate100:
                    interactableValue = -100;
                    break;
                
                    
            }
            interactableValueObserver?.Invoke(interactableValue);
            
        }
    }
}