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
        public static event Action<float> scoreChangedObserver;
        
        

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
            else if (playerChangeValue >= 100)
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
                //Win that Game
                GameManager.Instance.Won();
                Debug.Log("you win that game here you at x " + MiniGameManager.Instance.GetDiamondMultiplier());
            }
            else if (currentPlayerStateIndex >= 0)
            {
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
        }

        private void NextPlayer()
        {
            if (currentPlayerStateIndex < playerCount-1)
            {
                playerChangeValue = 0;
                currentPlayerStateIndex++;
                Debug.Log("next player");
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
            else if (currentPlayerStateIndex == playerCount - 1)
            {
                playerChangeValue = 100;
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
                true => 50,
                _ => 0
            };
            scoreChangedObserver?.Invoke(playerChangeValue);
        }
    }
}