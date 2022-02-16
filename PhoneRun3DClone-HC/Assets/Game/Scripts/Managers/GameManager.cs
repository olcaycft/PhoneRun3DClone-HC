using System;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int inGameDiamondCount;
        

        public static event Action GameStartObserver;
        private void Awake()
        {
            //DontDestroyOnLoad(gameObject);
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
        }
        public void StartThisLevel()
        {
            UIManager.Instance.StartGame();
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
            GameStartObserver?.Invoke();
        }
        public void ChangeDiamondCount(int index)
        {
            inGameDiamondCount += index;
            UIManager.Instance.InGameDiamond(inGameDiamondCount);
        }

        public void CurrentDiamondAtFinish(int index)
        {
            inGameDiamondCount *= index;
            PlayerPrefs.SetInt("DiamondCount", inGameDiamondCount + PlayerPrefs.GetInt("DiamondCount"));
            Won();
        }

        public void Won()
        {
            UIManager.Instance.Win();
            UIManager.Instance.FinishScore(inGameDiamondCount);
            UIManager.Instance.TotalDiamondCount();
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
        }

        public void Failed()
        {
            inGameDiamondCount = PlayerPrefs.GetInt("DiamondCount");
            UIManager.Instance.Fail();
        }

        

        
    }
}