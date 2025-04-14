using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    public AudioClip splashSound;
    public AudioClip windSound;
    public ParticleSystem windTrail;
    public ParticleSystem waterSplash;
    public ParticleSystem surfWake;
    public Material heroMaterial;

    private AudioSource playerAudio;
    private AudioSource mainCameraAudio;
    private Renderer playerRenderer;
    private new Rigidbody rigidbody;
    private PlayerController controller;
    private GameManager gameManager;
    private bool isGrounded = true;
    private bool hasPowerup = false;
    private int boundary = 4;
    private bool hasShield = false;

    public TextMeshProUGUI Collectible;
    public GameObject heart1, heart2, heart3;
    public GameObject ForceField;
    public GameObject destroyFX;

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

        if (transform.position.y < -5)
        {
            controller.enabled = false;
            mainCameraAudio.Stop();
            playerAudio.PlayOneShot(gameOverSound, 0.2f);
            gameManager.GameOver();
        }

        Vector3 playerPos = transform.position;

        if (playerPos.x > boundary)
        {
            playerPos.x = boundary;
        }
        else if (playerPos.x < -boundary)
        {
            playerPos.x = -boundary;
        }

        transform.position = playerPos;

        if(hasShield)
        {
            ForceField.transform.position = transform.position + new Vector3(0, 1, 0);
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true);
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            playerAudio.PlayOneShot(jumpSound, 0.1f);
            surfWake.Stop();
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
            playerAudio.PlayOneShot(splashSound, 0.1f);
            surfWake.Play();
            waterSplash.Play();
        }

        // The game restarts when the player hits an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if(hasShield)
            {
                Instantiate(destroyFX, collision.transform.position, Quaternion.identity);
                
                Destroy(collision.gameObject);
            }

            else
            {
                StartCoroutine(CollisionFlashRoutine());
                playerHealth--;
                UpdatePlayerHealthUI();
                playerAudio.PlayOneShot(collisionSound, 0.5f);

                if (playerHealth <= 0)
                {
                    controller.enabled = false;
                    mainCameraAudio.Stop();
                    playerAudio.PlayOneShot(gameOverSound, 0.2f);
                    gameManager.GameOver();
                }
                
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

            // update the collectibles number
            Collectible.SetText(" " + collectibles);

            playerAudio.PlayOneShot(collectibleSound, 0.1f);
        }

        // Controls the game's timescale to similuate the effect of a speed boost
        if (other.CompareTag("Speed Boost"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupSound, 0.7f);
            playerAudio.PlayOneShot(windSound, 0.3f);
            windTrail.Play();

            Time.timeScale *= 2f;
            StartCoroutine(SpeedBoostCountdownRoutine());
        }

         if (other.CompareTag("Shield"))
        {
            hasPowerup = true;
            hasShield = true;
            Debug.Log("Shield should show");
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupSound, 0.7f);

            ForceField.SetActive(true);
            StartCoroutine(ForceFieldCountDownRoutine());
        }
    }

    IEnumerator SpeedBoostCountdownRoutine()
    {
        yield return new WaitForSecondsRealtime(10);
        hasPowerup = false;
        Time.timeScale /= 2;
        windTrail.Stop();
    }


    IEnumerator CollisionFlashRoutine()
    {
        heroMaterial.color = Color.red;
        yield return new WaitForSecondsRealtime(0.3f);
        heroMaterial.color = Color.white;
    }

    IEnumerator ForceFieldCountDownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPowerup = false;
        hasShield = false;
        ForceField.SetActive(false);
    }

    void UpdatePlayerHealthUI()
    {
        switch (playerHealth)
        {
            case 0: // Game Over
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 1:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 2:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                break;
            case 3:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                break;
            default: // Game Over
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
        }
    }
}
