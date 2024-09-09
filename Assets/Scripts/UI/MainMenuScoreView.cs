using Game.Boot;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _scoreText;

        private void Start()
        {
            var bestScore = Score.Instance.Max;
            var lastScore = Mathf.Max(Score.Instance.Current, Score.Instance.Saved);

            var s = "";
            if (bestScore > 0)
            {
                s += $"Your Best Result: {bestScore} points";

                if (lastScore > 0)
                {
                    s += $"\nYour Last Result: {lastScore} points";
                }
            }

            _scoreText.text = s;
        }
    }
}
