using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject quitButton;


    void Start()
    {
        Screen.SetResolution(540, 960, false);
    }


    public void OnPlayButton()
    {
        Debug.Log("Play button clicked. Attempting to load scene 1.");
        SceneManager.LoadScene(1);
    }


    public void OnQuitButton()
    {
        Debug.Log("Quit button clicked. Attempting to quit the application.");
        Application.Quit();
    }


    public void DeletePlayerPrefs()
    {
        Debug.Log("Deleting all PlayerPrefs");
        PlayerPrefs.DeleteAll();
    }
}