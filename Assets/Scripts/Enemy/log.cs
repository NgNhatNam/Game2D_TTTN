using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public float attackCooldown = 1;


    //public Transform attackPoint;
    public Transform target;
    public Animator anim;


    private float attackCooldownTimer;
    private int facingDirection = -1;
    private EnemyState enemyState;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        CheckDistance();
    }

    public void CheckDistance()
    {
        //ChangeState(EnemyState.idle); 
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
                   Vector3.Distance(target.position, transform.position) >= attackRadius)
        {
        
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                        && currentState != EnemyState.stagger)
            {
                //rb.velocity = Vector2.zero;
                Chase();
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeState(EnemyState.walk);
            }
            else 
            {
                ChangeState(EnemyState.stagger);
            }
        }
        else if (
            Vector3.Distance(transform.position, target.transform.position) <= attackRadius && attackCooldownTimer <= 0)
        {
            attackCooldownTimer = attackCooldown;
            ChangeState(EnemyState.attack);
            
            //rb.velocity = Vector2.zero;
        }else
        {
            ChangeState(EnemyState.idle);
           //rb.velocity = Vector2.zero;
        }


    }
    /*
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, chaseRadius, );

        if (hits.Length > 0)
        {
            target = hits[0].transform;

            // Nếu player đang ở trong attack range và cooldown đã sẵn sàng
            if (Vector2.Distance(transform.position, target.position) <= attackRadius && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                currentState = EnemyState.attack;
                ChangeState(EnemyState.attack);
            }
            else if (Vector2.Distance(transform.position, target.position) > attackRadius && enemyState != EnemyState.attack)
            {
                currentState = EnemyState.walk;
                ChangeState(EnemyState.walk);
            }
           
        }
        else
        {
            currentState = EnemyState.idle;
            //rb.velocity = Vector2.zero;
            ChangeState(EnemyState.idle);

        }

    } */


    void Chase()
    {  
        if (target.position.x < transform.position.x && facingDirection == -1 ||
               target.position.x > transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
    // Lật nhân vật 
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void ChangeState(EnemyState newState)
    {
        // Thoát animation hiện tại
        if (enemyState == EnemyState.idle)
            anim.SetBool("EIdle", false);
        else if (enemyState == EnemyState.walk)
            anim.SetBool("EMove", false);
        else if (enemyState == EnemyState.attack)
            anim.SetBool("EAttack", false);
        else if (enemyState == EnemyState.stagger)
            anim.SetBool("EStagger", false);

        // Cập nhập trạng thái hiện tại
        enemyState = newState;

        // Thoát animation hiện tại
        if (enemyState == EnemyState.idle)
            anim.SetBool("EIdle", true);
        else if (enemyState == EnemyState.walk)
            anim.SetBool("EMove", true);
        else if (enemyState == EnemyState.attack)
            anim.SetBool("EAttack", true);
        else if (enemyState == EnemyState.stagger)
            anim.SetBool("EStagger", true);
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
