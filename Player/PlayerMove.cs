using UnityEngine;
using System.Collections;


public class PlayerMove : MonoBehaviour
{

    [Header("Moment Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    // private float checkRadius = 0.2f;
    // private Transform groundCheck;
    private Rigidbody2D body;
    private Animator amin;
    //private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    private float horizontalInput;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        amin = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Attack()
    {
        amin.SetTrigger("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        amin.SetBool("move", horizontalInput != 0);
        amin.SetBool("grounded", isGrounded());

        if (Input.GetKey(KeyCode.Space))
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
            jumpCounter = extraJumps;
        }

        if (Input.GetKeyDown(KeyCode.J) && canAttack())
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.L) && canDash)
        {
            StartCoroutine(Dash());
            amin.SetBool("Dashing", true);
            AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            if (audioManager != null && audioManager.dashClip != null)
            {
                audioManager.PlaySFX(audioManager.dashClip);
                // Debug.Log("Dash SFX played.");
            }
        }

    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && jumpCounter <= 0) return;

        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else
        {
            if (coyoteCounter > 0)
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                if (jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            }

        }
        amin.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = false;
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(
        capsuleCollider.bounds.center,
        capsuleCollider.bounds.size,
        CapsuleDirection2D.Vertical,
        0f,
        Vector2.down,
        0.1f,
        groundLayer
        );
        return raycastHit.collider != null;

    }

    public bool canAttack()
    {
        // return horizontalInput == 0;
        return true;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;

        amin.SetBool("Dashing", false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
