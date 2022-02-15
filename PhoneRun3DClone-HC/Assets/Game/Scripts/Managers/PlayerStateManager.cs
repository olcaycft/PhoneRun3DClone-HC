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
        //private GameObject currentPlayer;
        

        
        public static event Action<float> scoreChangedObserver;
        private PlayerStates currentPlayerStates;

        private void Awake()
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
            else if (playerChangeValue > 100)
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
           // currentPlayer = Instantiate(playerList[currentPlayerStateIndex].prefab, playerParent.transform.position,Quaternion.identity);
            //currentPlayer.transform.parent = playerParent.transform;
            //
            Debug.Log("player changed and current score = "+playerChangeValue);
        }
        
        private void ClearPlayerChangeScore()
        {
            playerChangeValue = 0;
            scoreChangedObserver?.Invoke(playerChangeValue);
        }

        private void SetPlayerCount(int count)
        {
            playerCount = count;
        }
    }

    
}