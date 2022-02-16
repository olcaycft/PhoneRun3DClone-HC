using Game.Scripts.Patterns;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private GameObject failUI;
        [SerializeField] private GameObject winUI;
        [SerializeField] private TextMeshProUGUI inGameDiamondTxt;
        [SerializeField] private TextMeshProUGUI totalDiamondTxt;
        [SerializeField] private TextMeshProUGUI finishScoreTxt;
        [SerializeField] private TextMeshProUGUI levelTxt;

        private int inGameDiamond;
        private void Awake()
        {
            startUI.SetActive(true);
            inGameUI.SetActive(true);
            TextCurrentLevel();
        }


        public void StartGame()
        {
            startUI.SetActive(false);
        }

        public void Fail()
        {
            inGameUI.SetActive(false);
            failUI.SetActive(true);
        }

        public void Win()
        {
            inGameUI.SetActive(false);
            winUI.SetActive(true);
        }

        public void InGameDiamond(int score)
        {
            this.inGameDiamond = score;
            inGameDiamondTxt.text = this.inGameDiamond.ToString();
            this.inGameDiamond = 0;
        }

        public void FinishScore(int score)
        {
            finishScoreTxt.text = score.ToString();
        }

        public void TotalDiamondCount()
        {
            totalDiamondTxt.text = $"{PlayerPrefs.GetInt("HighScore",0)}";
        }

        public void TextCurrentLevel()
        {
            levelTxt.text = $"{PlayerPrefs.GetInt("Level",1)}";
        }
    }
}
