using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    string highScoreKey = "HighScore";

    private void Update()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey, 0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(highScoreKey);
    }
}
