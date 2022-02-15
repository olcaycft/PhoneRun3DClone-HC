using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class MovableInteractable : InteractableBase
    {
        [SerializeField] private Transform sideMovementRoot;
        [SerializeField] private Transform gateRightLimit;
        [SerializeField] private Transform gateLeftLimit;
        private float gateRightLimitX => gateRightLimit.localPosition.x;
        private float gateLeftLimitX => gateLeftLimit.localPosition.x;
        private Vector3 startPoint;

        private Vector2 direction;

        private void Awake()
        {
            var pos = sideMovementRoot.position;
            pos.x = 0f;
            sideMovementRoot.position = pos;
            startPoint=sideMovementRoot.position;
            direction = Random.value < 0.5f ? Vector2.left : Vector2.right;
        }

        private void FixedUpdate()
        {
            Move();
        }


        public override void Move()
        {
            var time = Time.time;
            var pingPong = Mathf.PingPong(time, 1f);
            var minMax = Mathf.Lerp(gateLeftLimitX, gateRightLimitX, Mathf.SmoothStep(0.0f, 1f, pingPong));
            var gateMove = minMax * Vector3.left;
            sideMovementRoot.position = startPoint + gateMove;
        }
    }
}
