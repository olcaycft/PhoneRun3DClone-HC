using System.Collections;
using Game.Scripts.Managers;
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
        
        
        private int maxNumberForPlayerSwap=100;
        private void Awake()
        {
            InteractableController.ScoreChanged += ChangeBarPercentage;
        }

        private void OnDestroy()
        {
            InteractableController.ScoreChanged -= ChangeBarPercentage;
        }
        
        // karakter değişti eventi bekliyor.
        private void ChangePlayerName(string name)
        {
            currentPlayerName.text = name;
            ChangeBarPercentage(0);
        }
        private void ChangeBarPercentage(float scoreForSwap)
        {
            float currentPct = scoreForSwap / maxNumberForPlayerSwap;
            StartCoroutine(ChangeToPct(currentPct));
        }
    
        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = foregroundImage.fillAmount;
            float elapsed = 0f;
            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                yield return null;
            }
            if (pct<0)
                pct = 0;
            else if (pct>100)
                pct = 100;
            
            foregroundImage.fillAmount = pct;
        }
    }
}
