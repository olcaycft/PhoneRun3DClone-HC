using System;
using Game.Scripts.Patterns;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class MiniGameManager : MonoSingleton<MiniGameManager>
    {
        private int diamondMultiplier = 1;

        public static event Action miniGameStartedObserver;
        public static event Action playerAtFinishPartObserver;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetMultiplierValue(MiniGameStates state)
        {
            switch (state)
            {
                case MiniGameStates.MiniGameStart:
                    miniGameStartedObserver?.Invoke();
                    break;
                case MiniGameStates.X1Finish:
                case MiniGameStates.X2Finish:
                case MiniGameStates.X3Finish:
                case MiniGameStates.X4Finish:
                case MiniGameStates.X5Finish:
                    playerAtFinishPartObserver?.Invoke();
                    break;
            }

            diamondMultiplier = state switch
            {
                MiniGameStates.X1Enter => 1,
                MiniGameStates.X2Enter => 2,
                MiniGameStates.X3Enter => 3,
                MiniGameStates.X4Enter => 4,
                MiniGameStates.X5Enter => 5,
                MiniGameStates.X6Enter => 6,
                _ => diamondMultiplier
            };

            if (state==MiniGameStates.X6Finish)
            {
                //win direcly
                GameManager.Instance.Won();
                Debug.Log("finish the game and show win ui");
            }
        }

        public int GetDiamondMultiplier()
        {
            return diamondMultiplier;
        }
    }

    public enum MiniGameStates
    {
        MiniGameStart,
        X1Enter,
        X1Finish,
        X2Enter,
        X2Finish,
        X3Enter,
        X3Finish,
        X4Enter,
        X4Finish,
        X5Enter,
        X5Finish,
        X6Enter,
        X6Finish,
    }
}