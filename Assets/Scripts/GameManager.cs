using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private AudioSource sm64Pause;

    public bool isGameOver = false;
    public bool IsPaused { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (player == null || scoreText == null)
        {
            Debug.LogError("Player or ScoreText is not assigned in the inspector");
        }
    }


    private void Start()
    {
        InitializeGame();
    }


    private void Update()
    {
        if (player.IsDead && !isGameOver)
        {
            GameOver();
        }

        scoreText.text = player.CurrentScore.ToString();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
            {
                TogglePause();
            }
        }
    }


    private void InitializeGame()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
        if (scoreText != null)
            scoreText.enabled = true;

        ResetGameState();
        UpdatePauseUI();
    }


    private void ResetGameState()
    {
        player.CurrentScore = 0;
        isGameOver = false;
        IsPaused = false;
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        isGameOver = true;

        if (scoreText != null)
            scoreText.enabled = false;
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        Time.timeScale = 1f;
        SetPaused(false);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }


    public void TogglePause()
    {
        SetPaused(!IsPaused);
        sm64Pause.Play();
    }


    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        UpdatePauseUI();
        Debug.Log($"Game is {(IsPaused ? "paused" : "resumed")}. Time.timeScale = {Time.timeScale}");
    }


    private void UpdatePauseUI()
    {
        if (pauseButton != null && resumeButton != null)
        {
            pauseButton.SetActive(!IsPaused);
            resumeButton.SetActive(IsPaused);
        }
        else
        {
            Debug.Log("Pause or Resume button is not assigned");
        }
    }
}