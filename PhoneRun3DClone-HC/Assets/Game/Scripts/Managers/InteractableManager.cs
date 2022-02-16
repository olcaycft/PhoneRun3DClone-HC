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

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetInteractableValue(InteractableType state)
        {
            var interactableValue = state switch
            {
                InteractableType.Collectable3 => 80,
                InteractableType.Collectable10 => 10,
                InteractableType.Collectable15 => 15,
                InteractableType.Obstacle20 => -20,
                InteractableType.Obstacle40 => -40,
                InteractableType.ObstacleOldLady => -50,
                InteractableType.CollectableYoungLady => 50,
                InteractableType.GoodGate50 => 50,
                InteractableType.GoodGate100 => 100,
                InteractableType.BadGate50 => -50,
                InteractableType.BadGate100 => -100,
                _ => 0
            };
            interactableValueObserver?.Invoke(interactableValue);

            switch (state)
            {
                case InteractableType.Diamond:
                    GameManager.Instance.ChangeInGameDiamondCount(1);
                    break;
            }
        }
    }
    public enum InteractableType
    {
        Collectable3,
        Collectable10,
        Collectable15,
        Obstacle20,
        Obstacle40,
        ObstacleOldLady,
        CollectableYoungLady,
        Gate,
        GoodGate50,
        GoodGate100,
        BadGate50,
        BadGate100,
        Diamond
    }
}