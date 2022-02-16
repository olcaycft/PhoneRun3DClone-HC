using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.MiniGame
{
    public class MiniGameInteractableBase : MonoBehaviour
    {
        [SerializeField] protected MiniGameStates miniGameState;
        
        protected virtual void MoveTheDoors()
        {
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                MiniGameManager.Instance.SetMultiplierValue(miniGameState);
            }
        }
    }
}
