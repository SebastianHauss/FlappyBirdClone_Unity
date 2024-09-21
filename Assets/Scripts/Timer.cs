using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float counter = 5f;
    [SerializeField] private Text counterText;


    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            counterText.text = "Restarting in " + counter.ToString("0");
        }

        if (counter <= 0)
        {
            GameManager.Instance.RestartGame();
        }
    }
}