using Game.Boot;
using UnityEngine;

namespace Game
{
    public class ScoreSystem : MonoBehaviour
    {
        private void Awake()
        {
            EventEther.OnBrickBroken += HandleBrokenBrick;
            EventEther.OnLevelCompleted += HandleLevelCompete;
        }

        private void OnDisable()
        {
            EventEther.OnBrickBroken -= HandleBrokenBrick;
            EventEther.OnLevelCompleted -= HandleLevelCompete;
        }

        private static int CalcBonus(int brickPower)
        {
            if (brickPower >= 100) return 25;
            if (brickPower >= 50) return 10;
            return 5;
        }

        private void HandleBrokenBrick(Brick brick)
        {
            Score.Instance.Current += CalcBonus(brick.Power);
            Score.Instance.Max = Mathf.Max(Score.Instance.Current, Score.Instance.Max);
            EventEther.CallScoreChanged(Score.Instance.Current);
        }

        private void HandleLevelCompete(int levelIndex)
        {
            Score.Instance.Current += 50;
            Score.Instance.Max = Mathf.Max(Score.Instance.Current, Score.Instance.Max);
            EventEther.CallScoreChanged(Score.Instance.Current);
        }
    }
}

