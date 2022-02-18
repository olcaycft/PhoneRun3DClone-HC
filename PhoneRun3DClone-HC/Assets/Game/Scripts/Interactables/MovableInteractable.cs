using System;
using Game.Scripts.Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Interactables
{
    public class MovableInteractable : InteractableBase
    {
        [SerializeField] private Transform sideMovementRoot;
        [SerializeField] private Transform movableRightLimit;
        [SerializeField] private Transform movableLeftLimit;
        private float movableRightLimitX => movableRightLimit.localPosition.x;
        private float movableLeftLimitX => movableLeftLimit.localPosition.x;
        private float randomDirectionPercentage => SettingsManager.GameSettings.fiftyPercentageLuckValue;
        private Vector3 startPoint;

        private Vector3 direction;

        private void Awake()
        {
            var pos = sideMovementRoot.position;
            pos.x = 0f;
            sideMovementRoot.position = pos;
            startPoint=sideMovementRoot.position;
            direction = Random.value < randomDirectionPercentage ? Vector3.left : Vector3.right;
        }

        private void FixedUpdate()
        {
            Move();
        }

        protected override void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
            base.DoAction();
        }

        public override void Move()
        {
            var time = Time.time;
            var pingPong = Mathf.PingPong(time, 1f);
            var minMax = Mathf.Lerp(movableLeftLimitX, movableRightLimitX, Mathf.SmoothStep(0.0f, 1f, pingPong));
            var gateMove = minMax * direction;
            sideMovementRoot.position = startPoint + gateMove;
        }
    }
}
