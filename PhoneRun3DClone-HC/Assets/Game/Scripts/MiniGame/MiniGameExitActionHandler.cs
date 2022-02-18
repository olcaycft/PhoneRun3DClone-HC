using System;
using System.Collections;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.MiniGame
{
    public class MiniGameExitActionHandler : MiniGameInteractableBase
    {
        [SerializeField] private GameObject rightDoor;
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private bool isGameFinished;

        private void Start()
        {
            PlayerStateManager.playerStateChangedObserver += ChangePlayerStateIndex;
        }

        private void OnDestroy()
        {
            PlayerStateManager.playerStateChangedObserver -= ChangePlayerStateIndex;
        }

        private void ChangePlayerStateIndex(int index)
        {
            if (index == 0)
            {
                isGameFinished = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isGameFinished)
            {
                MoveTheDoors();
            }
        }


        protected override void MoveTheDoors()
        {
            StartCoroutine(nameof(OpenTheDoorsRoutine));
        }


        private IEnumerator OpenTheDoorsRoutine()
        {
            const float elapsed = 0.015f;
            while (leftDoor.transform.rotation.y > -45)
            {
                var currentLeftRotation = leftDoor.transform.rotation;
                var changedLeftRotation = currentLeftRotation;

                var currentRightRotation = rightDoor.transform.rotation;
                var changedRightRotation = currentRightRotation;

                changedLeftRotation.y -= 0.3f;
                currentLeftRotation.y = Mathf.Lerp(currentLeftRotation.y, changedLeftRotation.y, elapsed);
                leftDoor.transform.rotation = currentLeftRotation;

                changedRightRotation.y += 0.3f;
                currentRightRotation.y = Mathf.Lerp(currentRightRotation.y, changedRightRotation.y, elapsed);
                rightDoor.transform.rotation = currentRightRotation;

                yield return null;
            }

            StopCoroutine(nameof(OpenTheDoorsRoutine));
        }
    }
}