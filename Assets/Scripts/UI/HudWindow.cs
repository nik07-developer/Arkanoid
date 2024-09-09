using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class HudWindow : Window
    {
        [SerializeField]
        private TMP_Text _levelTxt;

        [SerializeField]
        private TMP_Text _livesTxt;

        [SerializeField]
        private TMP_Text _powerTxt;

        [SerializeField]
        private TMP_Text _scoreTxt;

        public void UpdateLevel(int level)
        {
            _levelTxt.text = $"Level: {level}";
        }

        public void UpdateLives(int lives)
        {
            _livesTxt.text = $"Lives: {lives}";
        }

        public void UpdatePower(int power)
        {
            _powerTxt.text = $"Power: {power}";
        }

        public void UpdateScore(int score)
        {
            _scoreTxt.text = $"Score: {score}";
        }
    }
}

