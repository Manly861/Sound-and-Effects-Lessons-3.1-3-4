using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = true;
    public ScoreManager scoreManager;
    private bool hasJumped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        scoreManager = GetComponent<ScoreManager>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver != false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasJumped = true;
            isOnGround = false;
            playerAim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            if (hasJumped && gameOver){
                scoreManager.UpdateScoreText(5);
            }
            
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = false;
            Debug.Log("Game Over!");
            playerAim.SetBool("Death_b", true);
            playerAim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            scoreManager.UpdateScoreText(0);

        }
    }
}
