using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class GateBase : InteractableBase
    {
        [SerializeField] private InteractableType rightGate;
        [SerializeField] private InteractableType leftGate;

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                switch (baseInteractableType)
                {
                    case InteractableType.Gate:
                    {
                        var pos=other.gameObject.transform.position;
                        baseInteractableType = pos.x<0 ? leftGate : rightGate;
                        break;
                    }
                }
                gameObject.SetActive(false);
            }
            base.OnTriggerEnter(other);
        }
    }
}
