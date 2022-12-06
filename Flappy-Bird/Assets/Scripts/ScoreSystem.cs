using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;
    string highScoreKey = "HighScore";

    private void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();

        if(score > PlayerPrefs.GetInt(highScoreKey, 0))
        {
            PlayerPrefs.SetInt(highScoreKey, score);
        }
    }
}
