using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Moment Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header ("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    // private float checkRadius = 0.2f;
    // private Transform groundCheck;
    private Rigidbody2D body;
    private Animator amin;
    //private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    private float horizontalInput;
    private bool grounded;

    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        amin = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        amin.SetBool("move", horizontalInput != 0);
        amin.SetBool("grounded", isGrounded());

        if (Input.GetKey(KeyCode.Space))
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if(isGrounded()){
            coyoteCounter = coyoteTime;
        } else {
            coyoteCounter -= Time.deltaTime;
            jumpCounter = extraJumps;
        }

    }

    private void Jump(){
        if (coyoteCounter <= 0 && jumpCounter <= 0) return;

        if(isGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        } else {
            if(coyoteCounter > 0)
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else {
                if(jumpCounter > 0){
                 body.velocity = new Vector2(body.velocity.x, jumpPower);
                 jumpCounter--;
                }
            }
  
        }
        amin.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision){
    if(collision.gameObject.tag == "Ground")
        grounded = false;
    }


    // void GroundCheck(){
    //     Collider2D[] colliders = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    //     if(colliders.Length > 0){
    //         foreach(var c in colliders){
    //             if(c.tag == "MovingPlatform")
    //                 transform.parent = c.transform;
    //     } 
    //     } else {
    //         transform.parent = null;
    //     }
    // }

    public bool isGrounded(){
        // RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, 
        // capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        // return raycastHit.collider != null;
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(
        capsuleCollider.bounds.center,  
        capsuleCollider.bounds.size,    
        CapsuleDirection2D.Vertical,   // 🎯 Thay 0 thành CapsuleDirection2D.Vertical hoặc Horizontal
        0f,                            // 🎯 Thay Vector2.down thành góc xoay (float)
        Vector2.down,                  // 🎯 Hướng cast (Vector2)
        0.1f,                          // 🎯 Khoảng cách cast (float)
        groundLayer   
        );
    return raycastHit.collider != null;  

    }

    public bool canAttack(){
        return horizontalInput == 0;
    }
}
