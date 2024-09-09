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

        private void HandleBrokenBrick(Brick brick)
        {
            Score.Instance.Current += brick.Power;
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

