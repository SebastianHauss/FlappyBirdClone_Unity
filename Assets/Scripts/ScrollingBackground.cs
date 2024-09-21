using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private readonly float speed = 0.2f;
    [SerializeField] private Renderer bgRenderer;


    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // make texture material scroll to the left on x-axis
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}