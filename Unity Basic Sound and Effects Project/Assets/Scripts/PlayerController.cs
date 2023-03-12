using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // To stores physics ref
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem dirtParticle; // Assign on editor
    private AudioSource playerAudio;
    public AudioClip jumpClip; // Assign on editor
    public AudioClip crashClip;

    private bool isOnGround = true;

    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get physics/anim
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpClip, 1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashClip, 1f);
        }
        
    }
}
