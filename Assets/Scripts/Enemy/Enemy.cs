using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,// Lảo đảo
    die,
}
public class Enemy : MonoBehaviour
{
    

    public EnemyState currentState;
    //public float maxHealth;
    public float health;
    public int score;
    public string enemyName;
    public float moveSpeed;

    
    public event Action OnDeath;
    public event Action<int> OnScoreReward;

    public EnemyState enemyState;


    private AudioManager audioManager;
    private Animator animator;


    private void Awake()
    {
        //health = maxHealth; lỗi
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            OnScoreReward += player.AddScore;
        }
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
        if (health <= 0)
        {
            currentState = EnemyState.die;
            Die();
        }
    }

    public void Die()

    {
        OnScoreReward?.Invoke(score); // Gửi điểm 

        OnDeath?.Invoke();

        // Xử lý khi player chết
        Debug.Log("Player đã chết!");
    }

    public void Knock(Rigidbody2D rb, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(rb, knockTime));
        TakeDamage(damage);
    }
    public IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null )
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
           // rb.velocity = Vector2.zero;
        }
    }

   
}
