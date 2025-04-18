using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float damage;


    public float weaponRange;
    public float knockBackForce;
    public float stuntime;

    public Log enemy;
    public Transform attackPoint;
    public LayerMask playerLayer;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    public void OrcAttack()
    {
        audioManager.PlaySFX(audioManager.orcAttack);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            PlayerHealth playerHealth = hits[0].GetComponent<PlayerHealth>();
            PlayerMovement playerKnockBack = hits[0].GetComponent<PlayerMovement>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            if (playerKnockBack != null)
            {
                playerKnockBack.KnockBack(transform, knockBackForce, stuntime);
            }
        }
    }

    public void BomerAttack()
    {
        audioManager.PlaySFX(audioManager.bomerAttack);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            PlayerHealth playerHealth = hits[0].GetComponent<PlayerHealth>();
            PlayerMovement playerKnockBack = hits[0].GetComponent<PlayerMovement>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            if (playerKnockBack != null)
            {
                playerKnockBack.KnockBack(transform, knockBackForce, stuntime);
            }
        }
    }

    public void BossAttack()
    {
        audioManager.PlaySFX(audioManager.bossAttack);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            PlayerHealth playerHealth = hits[0].GetComponent<PlayerHealth>();
            PlayerMovement playerKnockBack = hits[0].GetComponent<PlayerMovement>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            if (playerKnockBack != null)
            {
                playerKnockBack.KnockBack(transform, knockBackForce, stuntime);
            }
        }
    }   

    public void SoundBossDeath()
    {
        audioManager.PlaySFX(audioManager.bomerAttack);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
    /*-------------------------------------------------------
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }*/
}
