using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    private float jumpForce = 4f;
    private float moveSpeed = 10.0f;
    private int playerHealth = 3;
    public int collectibles = 0;

    public AudioClip collectibleSound;
    public AudioClip collisionSound;
    public AudioClip jumpSound;
    public AudioClip powerupSound;
    public AudioClip gameOverSound;

    private AudioSource playerAudio;
    private AudioSource mainCameraAudio;
    private new Rigidbody rigidbody;
    private PlayerController controller;
    private GameManager gameManager;
    private bool isGrounded = true;
    private bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        /* Vector3 move = new Vector3(horizontal, 0, 0).normalized * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + transform.TransformDirection(move));
        */

        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true);
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            playerAudio.PlayOneShot(jumpSound, 0.2f);
        }

        if (!Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // This allows the player to jump again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // The game restarts when the player hits an obstacle
        if (collision.gameObject.CompareTag("Obstacle") && hasPowerup == false)
        {
            playerHealth--;
            playerAudio.PlayOneShot(collisionSound, 0.2f);
            Debug.Log("You have " + playerHealth + " lives left.");

            if (playerHealth <= 0)
            {
                controller.enabled = false;
                mainCameraAudio.Stop();
                playerAudio.PlayOneShot(gameOverSound, 0.2f);
                gameManager.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collectibles are destroyed and then added to the player
        if (other.CompareTag("Collectible"))
        {
            collectibles++;
            Destroy(other.gameObject);
            Debug.Log("You have " + collectibles + " collectibles.");

            playerAudio.PlayOneShot(collectibleSound, 0.2f);
        }

        // Controls the game's timescale to similuate the effect of a speed boost
        if (other.CompareTag("Speed Boost"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            Debug.Log("You got a speed boost!");
            playerAudio.PlayOneShot(powerupSound, 0.7f);

            Time.timeScale = 2f;
            StartCoroutine(SpeedBoostCountdownRoutine());
        }

        // Lets the player collide with an obstacle once without taking damage
        if (other.CompareTag("Shield"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            Debug.Log("You got a shield!");

            playerAudio.PlayOneShot(powerupSound, 0.7f);

            StartCoroutine(ShieldCountdownRoutine());
        }

    }

    IEnumerator SpeedBoostCountdownRoutine()
    {
        yield return new WaitForSeconds(20);
        hasPowerup = false;
        Time.timeScale = 1;
    }

    IEnumerator ShieldCountdownRoutine()
    {
        yield return new WaitForSeconds(20);
        hasPowerup = false;
    }
}
