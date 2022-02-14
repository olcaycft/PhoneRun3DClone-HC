using System;
using Game.Scripts.Interactables;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class InteractableController : MonoBehaviour
    {
        [SerializeField] private float playerSwapScore;

        public static event Action<float> ScoreChanged;

        private void Awake()
        {
            Interactable.colisionWithInteractable += ChangePlayerChangeScore;
        }

        private void OnDestroy()
        {
            Interactable.colisionWithInteractable += ChangePlayerChangeScore;
        }

        private void ChangePlayerChangeScore(InteractableType state)
        {
            switch (state)
            {
                case InteractableType.Collectable3:
                    playerSwapScore += 3;
                    break;
                case InteractableType.Collectable10:
                    playerSwapScore += 10;
                    break;
                case InteractableType.Collectable15:
                    playerSwapScore += 15;
                    break;
                case InteractableType.Obstacle20:
                    playerSwapScore -= 20;
                    break;
                case InteractableType.ObstacleHacker:
                    playerSwapScore -= 50;
                    break;
            }
            ScoreChanged?.Invoke(playerSwapScore);
        }
    }
}
