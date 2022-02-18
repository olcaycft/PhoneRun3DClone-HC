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


        private int diamondValue => SettingsManager.GameSettings.diamondValue;
        private int miniCollectableValue => SettingsManager.GameSettings.miniCollectableValue;
        private int middleCollectableValue => SettingsManager.GameSettings.middleCollectableValue;
        private int bigCollectableValue => SettingsManager.GameSettings.bigCollectableValue;
        private int youngLadyCollectableValue => SettingsManager.GameSettings.youngLadyCollectableValue;
        private int miniObstacleValue => SettingsManager.GameSettings.miniObstacleValue;
        private int middleObstacleValue => SettingsManager.GameSettings.middleObstacleValue;
        private int hackerObstacleValue => SettingsManager.GameSettings.hackerObstacleValue;
        private int oldLadyObstacleValue => SettingsManager.GameSettings.oldLadyObstacleValue;
        private int goodGate50 => SettingsManager.GameSettings.goodGate50;
        private int goodGate100 => SettingsManager.GameSettings.goodGate100;
        private int badGate50 => SettingsManager.GameSettings.badGate50;
        private int badGate100 => SettingsManager.GameSettings.badGate100;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetInteractableValue(InteractableType state)
        {
            var interactableValue = state switch
            {
                InteractableType.MiniCollectable => miniCollectableValue,
                InteractableType.MiddleCollectable => middleCollectableValue,
                InteractableType.BigCollectable => bigCollectableValue,
                InteractableType.MiniObstacle => miniObstacleValue,
                InteractableType.MiddleObstacle => middleObstacleValue,
                InteractableType.HackerObstacle => hackerObstacleValue,
                InteractableType.ObstacleOldLady => oldLadyObstacleValue,
                InteractableType.CollectableYoungLady => youngLadyCollectableValue,
                InteractableType.GoodGate50 => goodGate50,
                InteractableType.GoodGate100 => goodGate100,
                InteractableType.BadGate50 => badGate50,
                InteractableType.BadGate100 => badGate100,
                _ => 0
            };
            interactableValueObserver?.Invoke(interactableValue);

            switch (state)
            {
                case InteractableType.Diamond:
                    GameManager.Instance.ChangeInGameDiamondCount(diamondValue);
                    break;
            }
        }
    }

    public enum InteractableType
    {
        MiniCollectable,
        MiddleCollectable,
        BigCollectable,
        MiniObstacle,
        MiddleObstacle,
        HackerObstacle,
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