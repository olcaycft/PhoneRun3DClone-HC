using System;
using System.Collections.Generic;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class PlayerStateManager : MonoBehaviour
    {
        [SerializeField] private float playerChangeValue;
        [SerializeField] private List<PlayerStates> playerStatesList;

        private int playerCount;
        private int currentPlayerStateIndex;
        private PlayerStates currentPlayerStates;
        private int playerSwapValue => SettingsManager.GameSettings.playerSwapValue;
        public static event Action<float> scoreChangedObserver;
        public static event Action<int> playerStateChangedObserver; 

        [SerializeField] private bool isMiniGameStart;

        private void Start()
        {
            playerCount = playerStatesList.Count;
            InteractableManager.interactableValueObserver += ChangeScoreValue;
            ChangePlayer(currentPlayerStateIndex);


            MiniGameManager.miniGameStartedObserver += ChangeMiniGameState;
            MiniGameManager.playerAtFinishPartObserver += PreviousPlayer;
        }

        private void OnDestroy()
        {
            InteractableManager.interactableValueObserver -= ChangeScoreValue;
            MiniGameManager.miniGameStartedObserver -= ChangeMiniGameState;
            MiniGameManager.playerAtFinishPartObserver -= PreviousPlayer;
        }

        private void ChangeScoreValue(float score)
        {
            playerChangeValue += score;

            scoreChangedObserver?.Invoke(playerChangeValue);

            if (playerChangeValue < 0)
            {
                PreviousPlayer();
            }
            else if (playerChangeValue >= playerSwapValue)
            {
                NextPlayer();
            }
        }


        private void PreviousPlayer()
        {
            //playerChangeValue = 0;
            currentPlayerStateIndex--;
            if (currentPlayerStateIndex < 0 && !isMiniGameStart)
            {
                Debug.Log("LevelFail");
                GameManager.Instance.Failed();
            }
            else if (currentPlayerStateIndex < 0 && isMiniGameStart)
            {
                
                GameManager.Instance.Won();
            }
            else if (currentPlayerStateIndex >= 0)
            {
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }

            if (!isMiniGameStart) return;
            playerStateChangedObserver?.Invoke(currentPlayerStateIndex);
        }

        private void NextPlayer()
        {
            if (currentPlayerStateIndex < playerCount - 1)
            {
                playerChangeValue = 0;
                currentPlayerStateIndex++;
                Debug.Log("next player");
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
            else if (currentPlayerStateIndex == playerCount - 1)
            {
                playerChangeValue = playerSwapValue;
            }
        }

        private void ChangeMiniGameState()
        {
            isMiniGameStart = true;
        }

        private void ChangePlayer(int currentPlayerStateIndex)
        {
            SpawnManager.Instance.SpawnRequest(playerStatesList[currentPlayerStateIndex]);
        }

        private void ClearPlayerChangeScore()
        {
            playerChangeValue = isMiniGameStart switch
            {
                true => playerSwapValue / 2,
                _ => 0
            };
            scoreChangedObserver?.Invoke(playerChangeValue);
        }
    }
}