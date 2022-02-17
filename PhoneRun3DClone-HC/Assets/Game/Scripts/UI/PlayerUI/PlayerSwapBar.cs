using System.Collections;
using Game.Scripts.Managers;
using Game.Scripts.Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.PlayerUI
{
    public class PlayerSwapBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPlayerName;
        [SerializeField] private Image foregroundImage;
        [SerializeField] private float updateSpeedSeconds = 0.2f;
        [SerializeField] private string add;

        private int maxNumberForPlayerSwap = 100;
        
        

        private void Awake()
        {
            PlayerStateManager.scoreChangedObserver += ChangeBarPercentage;
            SpawnManager.playerChangedObserver += ChangePlayerName;
        }

        private void OnDestroy()
        {
            PlayerStateManager.scoreChangedObserver -= ChangeBarPercentage;
            SpawnManager.playerChangedObserver -= ChangePlayerName;
        }

        
        private void ChangePlayerName(PlayerStates state)
        {
            //add = state.Equals(PlayerStates.Old.ToString()) || state.Equals(PlayerStates.Average.ToString()) ? "" : "!";
            add = state == PlayerStates.Old || state == PlayerStates.Slow ? "!" : "";
            currentPlayerName.text = state + add;
            
        }

        private void ChangeBarPercentage(float scoreForSwap)
        {
            var currentPct = scoreForSwap / maxNumberForPlayerSwap;
            StartCoroutine(ChangeToPct(currentPct));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            var preChangePct = foregroundImage.fillAmount;
            var elapse = 0f;
            while (elapse < updateSpeedSeconds)
            {
                elapse += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapse / updateSpeedSeconds);
                //foregroundImage.fillAmount = Mathf.InverseLerp(0f, pct, preChangePct);
                yield return null;
            }

            if (pct < 0)
                pct = 0;
            else if (pct >= 1)
                pct = 1;

            foregroundImage.fillAmount = pct;
        }
    }
}