using UnityEngine;

public class Fox : MonoBehaviour
{
    public float speed = 10;

    [SerializeField] bool isRunning = false;
    [Range(1f, 10f)] public float runSpeedMultiplier = 2f;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 300f;

    bool jumping = false;
    Rigidbody2D rb;
    float horizontalValue;
    float groundCheckRadius = 0.1f;
    [SerializeField] bool isGrounded = false;
    Animator anim;

    public AudioManager audioManager;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

        if (Input.GetKeyDown(KeyCode.Space)) jumping = true;
        if (Input.GetKeyUp(KeyCode.Space)) jumping = false;

        anim.SetBool("Jumping", !isGrounded && rb.linearVelocityY > 0.1f);
        anim.SetBool("Falling", !isGrounded && rb.linearVelocityY < 0.1f);
        anim.SetBool("isGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        Move(horizontalValue, jumping);
        GroundCheck();
    }

    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            audioManager.PlaySFX(audioManager.land);
            isGrounded = true;
        }
    }

    void Move(float dir, bool jumpCheck)
    {
        float xVal = dir * speed;

        if (isRunning) xVal *= runSpeedMultiplier;

        Vector2 targetVelocity = new Vector2(xVal, rb.linearVelocityY);
        rb.linearVelocity = targetVelocity;

        Vector3 scale = transform.localScale;
        if (dir > 0) scale.x = Mathf.Abs(scale.x);
        else if (dir < 0) scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;

        if (Mathf.Abs(dir) > 0.1f && isGrounded)
        {
            if (!audioManager.SFXSource.isPlaying) audioManager.PlaySFX(audioManager.walk);
        }


        if (jumpCheck && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            audioManager.PlaySFX(audioManager.jump);
            isGrounded = false;
            jumping = false;
        }

        anim.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocityX));
    }
}
