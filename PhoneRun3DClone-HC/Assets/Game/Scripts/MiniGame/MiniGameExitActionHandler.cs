using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.MiniGame
{
    public class MiniGameExitActionHandler : MiniGameInteractableBase
    {
        [SerializeField] private GameObject rightDoor;
        [SerializeField] private GameObject leftDoor;

        private void OnTriggerEnter(Collider other)
        {
            MoveTheDoors();
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