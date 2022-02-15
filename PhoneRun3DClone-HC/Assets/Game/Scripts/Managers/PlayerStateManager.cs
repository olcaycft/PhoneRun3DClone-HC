using System;
using System.Collections.Generic;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class PlayerStateManager : MonoBehaviour
    {
        [SerializeField] private float playerChangeValue;
        [SerializeField]private List<PlayerStates> playerStatesList;

        private int playerCount;
        private int currentPlayerStateIndex;
        
        public static event Action<float> scoreChangedObserver;
        private PlayerStates currentPlayerStates;

        private void Start()
        {
            playerCount = playerStatesList.Count;
            InteractableManager.interactableValueObserver += ChangeScoreValue;
            ChangePlayer(currentPlayerStateIndex);
        }

        private void OnDestroy()
        {
            InteractableManager.interactableValueObserver -= ChangeScoreValue;
        }

        private void ChangeScoreValue(float score)
        {
            playerChangeValue += score;
            
            scoreChangedObserver?.Invoke(playerChangeValue);
            
            if (playerChangeValue < 0)
            {
                PreviousPlayer();
            }
            else if (playerChangeValue >= 100)
            {
                NextPlayer();
            }
        }
        
        

        private void PreviousPlayer()
        {
            playerChangeValue = 0;
            currentPlayerStateIndex--;
            if (currentPlayerStateIndex < 0)
            {
                Debug.Log("LevelFail");
            }
            else
            {
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
        }

        private void NextPlayer()
        {
            playerChangeValue = 0;
            currentPlayerStateIndex++;
            if (currentPlayerStateIndex < playerCount)
            {
                Debug.Log("next player");
                //Destroy(currentPlayer);
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
        }

        private void ChangePlayer(int currentPlayerStateIndex)
        {
            SpawnManager.Instance.SpawnRequest(playerStatesList[currentPlayerStateIndex]);
        }
        
        private void ClearPlayerChangeScore()
        {
            playerChangeValue = 0;
            scoreChangedObserver?.Invoke(playerChangeValue);
        }

    }

    
}