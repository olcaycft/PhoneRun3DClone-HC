using System;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int inGameDiamondCount;
        

        public static event Action GameStartObserver;
        public static event Action levelFinishedObserver;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
        }
        public void StartThisLevel()
        {
            UIManager.Instance.StartGame();
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
            UIManager.Instance.InGameDiamond(inGameDiamondCount);
            GameStartObserver?.Invoke();
        }
        public void ChangeDiamondCount(int index)
        {
            inGameDiamondCount += index;
            UIManager.Instance.InGameDiamond(inGameDiamondCount);
        }


        public void Won()
        {
            UIManager.Instance.Win();
            UIManager.Instance.FinishScore(inGameDiamondCount);
            inGameDiamondCount *= MiniGameManager.Instance.GetDiamondMultiplier();
            PlayerPrefs.SetInt("DiamondCount", inGameDiamondCount + PlayerPrefs.GetInt("DiamondCount"));
            
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