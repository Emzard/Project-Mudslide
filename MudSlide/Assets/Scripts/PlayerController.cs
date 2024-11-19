using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float jumpForce = 5.0f;
    private float moveSpeed = 10.0f;
    public int collectibles = 0;

    public AudioClip collectibleSound;
    public AudioClip collisionSound;
    public AudioClip jumpSound;
    public AudioClip powerupSound;
    public AudioClip gameOverSound;

    private AudioSource playerAudio;
    private AudioSource mainCameraAudio;
    private new Rigidbody rigidbody;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
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
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            playerAudio.PlayOneShot(jumpSound, 0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // This allows the player to jump again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Prints game over when the player hits an obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(collisionSound, 0.2f);
            playerAudio.PlayOneShot(gameOverSound, 0.2f);
            mainCameraAudio.Stop();

            Debug.Log("Game Over!");
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

        if (other.CompareTag("Speed Boost"))
        {
            Destroy(other.gameObject);
            Debug.Log("You got a speed boost!");

            playerAudio.PlayOneShot(powerupSound, 0.7f);
        }

        if (other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);
            Debug.Log("You got a shield!");

            playerAudio.PlayOneShot(powerupSound, 0.7f);
        }
    }

    /* private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    } */
}
