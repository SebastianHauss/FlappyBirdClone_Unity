using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 1.5f;
    private readonly float spawnHeight = 0.4f;
    private float timer = 0;


    private void Update()
    {
        if (GameManager.Instance.isGameOver == true) return;

        timer += Time.deltaTime;

        if (timer > spawnInterval)

        {    // Spawn a new pipe and position it randomly within the height range
            GameObject newPipe = Instantiate(pipePrefab);

            // Set the position of the pipe (random y-position)
            newPipe.transform.position = new Vector3(transform.position.x, Random.Range(-spawnHeight, spawnHeight), 0.25f);

            // Reset the timer after spawning
            timer = 0;

            // Destroy the pipe after 15 seconds
            Destroy(newPipe, 15);
        }
    }
}
