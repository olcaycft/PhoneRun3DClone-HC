using System;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class InteractableController : MonoBehaviour
    {
        private float playerSwapScore;

        public static event Action<float> interactableValue;

        private void Awake()
        {
            Interactable.colisionWithInteractable += ChangePlayerChangeScore;
            //PlayerStateManager.playerChanged += ClearScore;
        }

        private void OnDestroy()
        {
            Interactable.colisionWithInteractable += ChangePlayerChangeScore;
            //PlayerStateManager.playerChanged += ClearScore;
        }

        private void ChangePlayerChangeScore(InteractableType state)
        {
            var scoreChangeValue = 0;
            switch (state)
            {
                case InteractableType.Collectable3:
                    scoreChangeValue = 80;
                    break;
                case InteractableType.Collectable10:
                    scoreChangeValue = 10;
                    break;
                case InteractableType.Collectable15:
                    scoreChangeValue = 15;
                    break;
                case InteractableType.Obstacle20:
                    scoreChangeValue = -20;
                    break;
                case InteractableType.ObstacleHacker:
                    scoreChangeValue = -40;
                    break;
                case InteractableType.GoodGate50:
                    scoreChangeValue = 50;
                    break;
                case InteractableType.GoodGate100:
                    scoreChangeValue = 100;
                    break;
                case InteractableType.BadGate50:
                    scoreChangeValue = -50;
                    break;
                case InteractableType.BadGate100:
                    scoreChangeValue = -100;
                    break;
                
                    
            }
            interactableValue?.Invoke(scoreChangeValue);
            
        }

        /*private void ClearScore(string state)
        {
            playerSwapScore = 0;
            scoreChanged?.Invoke(playerSwapScore);
        }*/
    }
}