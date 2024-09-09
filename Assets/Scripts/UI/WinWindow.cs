using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class WinWindow : Window
    {
        [SerializeField]
        private TMP_Text _finalText;

        public void ShowFinalScore(int score, int bestScore)
        {
            _finalText.text = $"You Win the Game!\n Your Score: {score}";
        }
    }
}

