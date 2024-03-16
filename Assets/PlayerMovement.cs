using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private bool isJumping;
    private int jumpCount = 0;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private Vector3 minScreenPos;
    private Vector3 maxScreenPos;
    private Animator p_Animator = null;    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
     
    // private void Start()
    // {
    //     p_Animator = GetComponent<Animator>();
    // }

    private void Start()
    {
        // 计算屏幕的最小和最大坐标
        minScreenPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxScreenPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        p_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // 检查玩家的坐标是否超出屏幕范围
        if (transform.position.x < minScreenPos.x)
        {
            transform.position = new Vector3(minScreenPos.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxScreenPos.x)
        {
            transform.position = new Vector3(maxScreenPos.x, transform.position.y, transform.position.z);
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            p_Animator.SetTrigger("Run");
            p_Animator.SetBool("Fall", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            p_Animator.SetTrigger("Jump");
            p_Animator.SetBool("Fall", true);
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping && jumpCount < 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());

            jumpCount++;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y - 0.5f);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpCount = 0;
            p_Animator.SetBool("Fall", false);
        }
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Ground") 
    //     {
    //         p_Animator.SetBool("Fall", false);
    //     }   
    // }

}