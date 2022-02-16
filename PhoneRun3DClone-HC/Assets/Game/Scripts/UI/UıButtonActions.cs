using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UıButtonActions : MonoBehaviour
    {
        public void PlayCurrentLevelAgain()
        {
            LevelManager.Instance.PlayCurrentLevel();
        }

        public void PlayNextLevel()
        {
            LevelManager.Instance.PlayNextLevel();
        }
    }
}
