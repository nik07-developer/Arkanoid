using Game.Boot;
using UnityEngine;

namespace Game
{
    public class ScoreSystem : MonoBehaviour
    {
        private void Awake()
        {
            EventEther.OnBrickBroken += HandleBrokenBrick;
        }

        private void OnDisable()
        {
            EventEther.OnBrickBroken -= HandleBrokenBrick;
        }

        private void HandleBrokenBrick(Brick brick)
        {
            Score.Instance.Current += 5;
            EventEther.CallScoreChanged(Score.Instance.Current);
        }

        private void HandleLevelCompete(int levelIndex)
        {
            Score.Instance.Current += 50;
        }
    }
}

