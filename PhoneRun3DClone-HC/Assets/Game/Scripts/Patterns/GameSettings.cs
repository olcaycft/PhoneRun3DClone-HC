using Game.Scripts.Managers;
using TMPro.Examples;
using UnityEngine;

namespace Game.Scripts.Patterns
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        private InteractableType interactableType;
        [Header("Random Value Generating")] 
        public float fiftyPercentageLuckValue = 0.5f;

        [Header("Interactable Values")] 
        public int diamondValue = 1;
        public int miniCollectableValue = 3;
        public int middleCollectableValue = 10;
        public int bigCollectableValue = 15;
        public int youngLadyCollectableValue = 50;
        public int miniObstacleValue = -10;
        public int middleObstacleValue = -20;
        public int hackerObstacleValue = -40;
        public int oldLadyObstacleValue = -50;
        public int goodGate50 = 50;
        public int goodGate100 = 100;
        public int badGate50 = -50;
        public int badGate100 = -100;

        [Header("Player Swap Value")] 
        public int playerSwapValue = 100;

        [Header("Player Movement Values")]
        public float playerSideMovementSensitivity = 10f;
        public float playerSideMovementLerpSpeed = 5f;
        public float playerForwardSpeed = 5f;


    }
}
