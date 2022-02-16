using System;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        private GameObject currentPlayer;
        [SerializeField] private GameObject playerParent;
        public static event Action<PlayerStates> playerChangedObserver;

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
            
            playerChangedObserver?.Invoke(state);
        }
    }
}