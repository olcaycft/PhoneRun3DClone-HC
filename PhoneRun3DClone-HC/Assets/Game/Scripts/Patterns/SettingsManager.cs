using UnityEngine;

namespace Game.Scripts.Patterns
{
    public class SettingsManager : MonoSingleton<SettingsManager>
    {
        [SerializeField] private GameSettings settings;
        public static GameSettings GameSettings => Instance.settings;
    }
}