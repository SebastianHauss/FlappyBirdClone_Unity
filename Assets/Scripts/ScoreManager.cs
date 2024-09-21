using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highscoreText;
    private Player player;
    public int Highscore { get; private set; }


    void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();

        Highscore = PlayerPrefs.GetInt("highscore", 0);
    }


    private void Start()
    {
        scoreText.text = player.CurrentScore.ToString("0");
        highscoreText.text = Highscore.ToString("0");
    }


    public void AddPoint()
    {
        player.CurrentScore++;
        scoreText.text = player.CurrentScore.ToString("0");

        if (Highscore < player.CurrentScore)
        {
            PlayerPrefs.SetInt("highscore", player.CurrentScore);
            PlayerPrefs.Save();

            Highscore = player.CurrentScore;

            highscoreText.text = Highscore.ToString("0");

        }
    }
}