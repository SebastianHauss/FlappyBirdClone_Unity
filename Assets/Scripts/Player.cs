using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // --- Player private variables ---
    private Rigidbody2D rb;
    private GameManager gameManager;
    private Animator animator;
    [SerializeField] private float jumpForce = 1.3f;
    [SerializeField] private float rotationSpeed = 10f;
    private bool jumpRequested = false;


    // --- Properties (Getters and Setters)---
    public int CurrentScore { get; set; }
    public bool IsDead { get; set; }


    // --- Audio Components ---
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;


    private enum Sound
    {
        Wing, // 0 
        Hit, // 1
        Point, // 2
        Die // 3
    }


    private void Start()
    {
        IsDead = false;

        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }


    private void Update()
    {
        if (IsDead == true) return;

        // Check for mouse click to request a jump
        if (Input.GetMouseButtonDown(0))
        {
            jumpRequested = true;
        }

        // Rotate player based on velocity
        float angle = Mathf.Clamp(rb.velocity.y * rotationSpeed, -90, 45);
        transform.rotation =
            Quaternion.Euler(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, Time.deltaTime * rotationSpeed));
    }


    private void FixedUpdate()
    {
        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }
    }


    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;

        animator.SetTrigger("Flap");
        PlaySound(Sound.Wing);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsDead && collision.gameObject.CompareTag("Ground"))
        {
            IsDead = true;
            rb.velocity = Vector2.zero;
            rb.freezeRotation = true;

            PlaySound(Sound.Hit);

            gameManager.GameOver();
        }

        if (!IsDead && collision.gameObject.CompareTag("Pipe"))
        {
            IsDead = true;
            rb.velocity = Vector2.zero;
            rb.freezeRotation = true;

            PlaySound(Sound.Hit);
            StartCoroutine(PlayDieSoundAfterDelay(0.3f));

            gameManager.GameOver();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            ScoreManager.instance.AddPoint();

            PlaySound(Sound.Point);
        }
    }


    private void PlaySound(Sound sound)
    {
        if (audioClips[(int)sound] != null)
        {
            audioSource.PlayOneShot(audioClips[(int)sound]);
        }
        else
        {
            Debug.LogWarning(sound.ToString() + " sound is not assigned");
        }
    }


    private IEnumerator PlayDieSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(Sound.Die);
    }
}