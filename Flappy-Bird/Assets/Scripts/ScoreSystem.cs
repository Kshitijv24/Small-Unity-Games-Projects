using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;
    string highScoreKey = "HighScore";
    int backgroundNumberIncrement = 0;

    private void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncrementScore()
    {
        score++;
        backgroundNumberIncrement++;
        scoreText.text = "Score: " + score.ToString();

        if(score > PlayerPrefs.GetInt(highScoreKey, 0))
        {
            PlayerPrefs.SetInt(highScoreKey, score);
        }

        if (backgroundNumberIncrement == 5)
        {
            FindObjectOfType<Background>().ChangeBackground();
        }
        else if(backgroundNumberIncrement == 10)
        {
            FindObjectOfType<Background>().ChangeBackground();
        }
        else if (backgroundNumberIncrement == 15)
        {
            FindObjectOfType<Background>().ChangeBackground();
        }
        else if (backgroundNumberIncrement == 20)
        {
            FindObjectOfType<Background>().ChangeBackground();
        }
        else if (backgroundNumberIncrement == 25)
        {
            FindObjectOfType<Background>().ChangeBackground();
        }
        else if (backgroundNumberIncrement == 30)
        {
            FindObjectOfType<Background>().ChangeBackground();
            backgroundNumberIncrement = 0;
        }
    }
}
