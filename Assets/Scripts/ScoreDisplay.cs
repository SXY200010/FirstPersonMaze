using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;

    void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
        UpdateScore(ScoreManager.Score);
    }

    void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }
}
