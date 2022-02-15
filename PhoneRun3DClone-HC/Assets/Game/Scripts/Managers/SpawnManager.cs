using System;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        private GameObject currentPlayer;
        [SerializeField] private GameObject playerParent;
        private PlayerStates states;
        public static event Action<PlayerStates> playerChanged;

        public void SpawnRequest(PlayerStates state)
        {
            if (currentPlayer != null)
            {
                currentPlayer.transform.SetParent(null);
                currentPlayer.transform.position = Vector3.zero;
                currentPlayer.SetActive(false);
            }
            
            currentPlayer =
                ObjectPooler.Instance.SpawnFromPool(state, playerParent.transform.position, Quaternion.identity);
            currentPlayer.transform.parent = playerParent.transform;
            
            playerChanged?.Invoke(state);
        }
    }
}