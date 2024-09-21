using UnityEngine;

public class MovePipes : MonoBehaviour
{
    private readonly float speed = 0.5f;


    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // Move the obstacle pipes to the left on x-axis
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }
}