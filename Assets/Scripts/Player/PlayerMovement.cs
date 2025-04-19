using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public enum PlayerState
{
    idle,
    walk,
    attack,
    stagger,
    interract, // tuong tac
    dashing,
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    //public Signals playerHealthSignal;
    public PlayerState currentState;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private Animator anim;
   
    private bool isKnockedBack;
    // [SerializeField]
    public float coolDown;
    private float timer;
    
    /*
    // Dash 
    public float dashSpeed = 10f; // Tốc độ dash
    public float dashDuration = 0.2f; // Thời gian dash
    public float dashCooldown = 1f; // Thời gian cooldown giữa các lần dash
    private bool isDashing = false; // Trạng thái dash
    private float dashTimer = 0f; // Đếm ngược cooldown
    */


    public int facingDirection = 1; // 1: nhìn phải, -1: nhìn trái
    public Hand hand; // Tham chiếu đến Hand\


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        currentState = PlayerState.idle;
    }
     void Update()
    {
        if(Time.timeScale != 0)
        {// Giảm timer nếu nó lớn hơn 0
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            /*/ Giảm cooldown dash
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
            }*/

            FlipToMouseDirection();

            // Kiểm tra nếu nhấn chuột trái
            if (Input.GetMouseButtonDown(0) && timer <= 0)
            {
                timer = coolDown;
                //currentState = PlayerState.attack;
                hand.isAttack();
            }
            else
            {
                hand.FinishAttack();
            }

            /*/ Dash khi nhấn Shift
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0 && !isDashing)
            {
                StartCoroutine(Dash());
            }*/
        }
        else
        {
            Time.timeScale = 0f;
        }

    }

    void FixedUpdate()
    {
        MovementPlayer();
        
    }

    public void MovementPlayer()
    {
        if (isKnockedBack == false)
        {
            //if(!isDashing) { 
                currentState = PlayerState.walk;
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                // Cập nhật animation dựa trên hướng di chuyển
                anim.SetFloat("horizontal", Mathf.Abs(horizontal));
                anim.SetFloat("vertical", Mathf.Abs(vertical));
                rb.velocity = new Vector2(horizontal, vertical) * speed;

                // Kiểm tra xem player có đang di chuyển hay không
                if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
                {
                    currentState = PlayerState.walk;
                }
                else
                {
                    currentState = PlayerState.idle;
                }
            //}
        }
        else
        {
            
            anim.SetBool("isStagger", true);
            currentState = PlayerState.stagger; 
        }
    }
    public void FlipToMouseDirection()
    {
        // Lấy vị trí con trỏ chuột trong không gian thế giới
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Đảm bảo z = 0 để tránh vấn đề về độ sâu

        // Kiểm tra xem chuột nằm bên trái hay bên phải Player
        if (mousePosition.x < transform.position.x && facingDirection > 0)
        {
            // Chuột ở bên trái và Player đang nhìn phải -> Lật sang trái
            Flip();
        }
        else if (mousePosition.x > transform.position.x && facingDirection < 0)
        {
            // Chuột ở bên phải và Player đang nhìn trái -> Lật sang phải
            Flip();
        }
    }

    // Function dùng để lật nhân vật
    public void Flip()
    {
        // Đảo ngược hướng
        facingDirection *= -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    //-----------------------------------------------------------------------------
    public void KnockBack(Transform enemy, float force, float stuntime)
    {
        isKnockedBack = true;
        anim.SetBool("isStagger", true);
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockBackCounter(stuntime));
    }

    IEnumerator KnockBackCounter(float stuntime)
    {
        yield return new WaitForSeconds(stuntime);
        anim.SetBool("isStagger", false);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    /*
    IEnumerator Dash()
    {
        isDashing = true;

        currentState = PlayerState.dashing; // Chuyển trạng thái sang dash

        // Lưu hướng hiện tại của nhân vật
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 dashDirection = new Vector2(horizontal, vertical).normalized;

        // Nếu không có hướng di chuyển, sử dụng hướng nhìn hiện tại
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(facingDirection, 0);
        }

        // Thực hiện dash
        anim.SetBool("isDash", true);
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Kết thúc dash
        rb.velocity = Vector2.zero;
        isDashing = false;
        anim.SetBool("isDash", false);
        currentState = PlayerState.idle;

        // Bắt đầu cooldown
        dashTimer = dashCooldown;
    }
    */

}
