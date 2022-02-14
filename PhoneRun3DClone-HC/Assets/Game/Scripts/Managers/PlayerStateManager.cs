using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Scripts.Interactables;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class PlayerStateManager : MonoBehaviour
    {
        [SerializeField] private float playerChangeScore;
        public List<Player> playerList;
        private int currentPlayerStateIndex;


        private GameObject currentPlayer;
        [SerializeField] private GameObject playerParent;

        public static event Action<PlayerStates> playerChanged;
        public static event Action<float> scoreChanged;
        private PlayerStates currentPlayerStates;

        private void Awake()
        {
            InteractableController.interactableValue += ChangeScoreValue;

            ChangePlayer(currentPlayerStateIndex);
        }

        private void OnDestroy()
        {
            InteractableController.interactableValue -= ChangeScoreValue;
        }

        private void ChangeScoreValue(float score)
        {
            playerChangeScore += score;
            
            scoreChanged?.Invoke(playerChangeScore);
            
            if (playerChangeScore < 0)
            {
                PreviousPlayer();
            }
            else if (playerChangeScore > 100)
            {
                NextPlayer();
            }
        }
        
        

        private void PreviousPlayer()
        {
            playerChangeScore = 0;
            currentPlayerStateIndex--;
            if (currentPlayerStateIndex < 0)
            {
                Debug.Log("LevelFail");
            }
            else
            {
                Destroy(currentPlayer);
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
        }

        private void NextPlayer()
        {
            playerChangeScore = 0;
            currentPlayerStateIndex++;
            if (currentPlayerStateIndex < playerList.Count)
            {
                Destroy(currentPlayer);
                ClearPlayerChangeScore();
                ChangePlayer(currentPlayerStateIndex);
            }
        }

        private void ChangePlayer(int currentPlayerStateIndex)
        {
            currentPlayer = Instantiate(playerList[currentPlayerStateIndex].prefab, playerParent.transform.position,
                Quaternion.identity);
            currentPlayer.transform.parent = playerParent.transform;
            playerChanged?.Invoke(playerList[currentPlayerStateIndex].state);
        }
        
        private void ClearPlayerChangeScore()
        {
            playerChangeScore = 0;
            scoreChanged?.Invoke(playerChangeScore);
        }
    }

    [System.Serializable]
    public class Player
    {
        public PlayerStates state;
        public GameObject prefab;
    }

    public enum PlayerStates
    {
        Old,
        Slow,
        Average,
        Modern,
        Futuristic
    }
}