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

    //public Text peopleSaved;
    //public Text health;
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
    private float soundVolume;

    public TextMeshProUGUI Collectible;
    public GameObject heart1, heart2, heart3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        controller = GetComponent<PlayerController>();
        GetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player updated the volume settings
        if (PlayerPrefs.HasKey("volume-isupdated"))
        {
            int isVolumeUpdated = PlayerPrefs.GetInt("volume-isupdated");
            if (isVolumeUpdated == 1)
            {
                GetVolume();
            } 
        } else
        {
            PlayerPrefs.SetInt("volume-isupdated", 0);
        }
        
        Move();
        Jump();

        if (transform.position.y < -5)
        {
            controller.enabled = false;
            mainCameraAudio.Stop();
            playerAudio.PlayOneShot(gameOverSound, soundVolume);//0.2f);
            gameManager.GameOver();
        }
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

            playerAudio.PlayOneShot(jumpSound, soundVolume);//0.2f);
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
            UpdatePlayerHealthUI();
            //health.text = "Health: " + playerHealth;
            playerAudio.PlayOneShot(collisionSound, soundVolume);//0.2f);

            if (playerHealth <= 0)
            {
                controller.enabled = false;
                mainCameraAudio.Stop();
                playerAudio.PlayOneShot(gameOverSound, soundVolume);//0.2f);
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
            //peopleSaved.text = "People: " + collectibles;

            // update the collectibles number
            Collectible.SetText(" " + collectibles);

            playerAudio.PlayOneShot(collectibleSound, soundVolume);//0.2f);
        }

        // Controls the game's timescale to similuate the effect of a speed boost
        if (other.CompareTag("Speed Boost"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupSound, soundVolume);//0.7f);

            Time.timeScale = 2f;
            StartCoroutine(SpeedBoostCountdownRoutine());
        }

        // Lets the player collide with an obstacle once without taking damage
        if (other.CompareTag("Shield"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);

            playerAudio.PlayOneShot(powerupSound, soundVolume);// 0.7f);

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

    void GetVolume()
    {
        if (PlayerPrefs.HasKey("music-volume"))
        {
            //set the volume
            mainCameraAudio.volume = PlayerPrefs.GetFloat("music-volume");
        }
        else
        {
            mainCameraAudio.volume = 0.5f;
            PlayerPrefs.SetFloat("music-volume", 0.5f);
        }

        if (PlayerPrefs.HasKey("sound-volume"))
        {
            //set the volume
            soundVolume = PlayerPrefs.GetFloat("sound-volume");
        }
        else
        {
            soundVolume = 0.5f;
            PlayerPrefs.SetFloat("sound-volume", 0.5f);
        }
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
