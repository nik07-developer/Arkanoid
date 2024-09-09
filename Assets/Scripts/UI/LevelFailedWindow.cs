using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class LevelFailedWindow : Window
    {
        [SerializeField]
        private TMP_Text _text;

        public void ShowFailText(int lives)
        {
            _text.text = $"Ops, try againg.\nYou have {lives} lives";
        }
    }
}
