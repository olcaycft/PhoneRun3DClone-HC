using System;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int inGameDiamondCount;
        

        public static event Action gameStartObserver;
        public static event Action levelFinishedObserver;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            inGameDiamondCount = 0;
        }
        public void StartThisLevel()
        {
            UIManager.Instance.StartGame();
            inGameDiamondCount = 0;
            UIManager.Instance.TotalDiamondTextInGameUI();
            gameStartObserver?.Invoke();
        }
        public void ChangeInGameDiamondCount(int index)
        {
            inGameDiamondCount += index;
        }


        public void Won()
        {
            UIManager.Instance.Win();
            UIManager.Instance.CollectedDiamondsInGame(inGameDiamondCount);
            inGameDiamondCount *= MiniGameManager.Instance.GetDiamondMultiplier();
            PlayerPrefs.SetInt("DiamondCount", inGameDiamondCount + PlayerPrefs.GetInt("DiamondCount"));
            UIManager.Instance.TotalDiamondTextInWınUI();
            levelFinishedObserver?.Invoke();
        }

        public void Failed()
        {
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
            UIManager.Instance.Fail();
            
            levelFinishedObserver?.Invoke();
        }

        

        
    }
}