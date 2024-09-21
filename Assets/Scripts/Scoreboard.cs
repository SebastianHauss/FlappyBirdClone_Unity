using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard instance;
    private Player player;
    public Text scoreTextScoreboard;
    private int score;
    private int highScore;
    [SerializeField] private Sprite[] medalSprites;
    [SerializeField] private Image medalImage;
    [SerializeField] private Image highScoreImage;
    private MedalType currentMedal;
    private bool isNewHighScore = false;

    enum MedalType
    {
        None,
        Bronze,
        Silver,
        Gold
    }


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player object not found in the scene!");
            return;
        }

        if (highScoreImage == null)
        {
            Debug.LogError("High Score Image is not assigned in the inspector!");
        }
        else
        {
            highScoreImage.gameObject.SetActive(false);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        score = player.CurrentScore;
        UpdateScoreOnScoreboard();
    }


    void Update()
    {
        if (player == null) return;

        score = player.CurrentScore;
        UpdateScoreOnScoreboard();
        CheckForNewHighScore();
        DetermineMedalBasedOnScore();
        UpdateMedalImage();
    }


    private void UpdateScoreOnScoreboard()
    {
        scoreTextScoreboard.text = score.ToString("0");
    }


    public void CheckForNewHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > currentHighScore)
        {
            Debug.Log("New high score achieved!");
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();

            if (!isNewHighScore)
            {
                isNewHighScore = true;
                ShowHighScoreImage();
            }
        }

        else if (score < currentHighScore)
        {
            if (isNewHighScore)
            {
                isNewHighScore = false;
                HideHighScoreImage();
            }
        }
    }


    private void ShowHighScoreImage()
    {
        Debug.Log("Attempting to show high score image");
        if (highScoreImage != null)
        {
            highScoreImage.gameObject.SetActive(true);
            Debug.Log("High score image should now be visible");
        }
        else
        {
            Debug.LogError("High Score Image is null!");
        }
    }


    private void HideHighScoreImage()
    {
        Debug.Log("Hiding high score image");
        if (highScoreImage != null)
        {
            highScoreImage.gameObject.SetActive(false);
        }
    }


    private void DetermineMedalBasedOnScore()
    {
        if (player.CurrentScore >= 100)
        {
            currentMedal = MedalType.Gold;
        }
        else if (player.CurrentScore >= 75)
        {
            currentMedal = MedalType.Silver;
        }
        else if (player.CurrentScore >= 50)
        {
            currentMedal = MedalType.Bronze;
        }
        else
        {
            currentMedal = MedalType.None;
        }
    }


    private void UpdateMedalImage()
    {
        if (medalSprites.Length < 3)
        {
            Debug.LogError("Not enough medal sprites assigned!");
            return;
        }

        switch (currentMedal)
        {
            case MedalType.Bronze:
                medalImage.sprite = medalSprites[0];
                medalImage.enabled = true;
                break;
            case MedalType.Silver:
                medalImage.sprite = medalSprites[1];
                medalImage.enabled = true;
                break;
            case MedalType.Gold:
                medalImage.sprite = medalSprites[2];
                medalImage.enabled = true;
                break;
            case MedalType.None:
            default:
                medalImage.enabled = false;
                break;
        }
    }
}