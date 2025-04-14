using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int score;

    public bool die = false;

    public SpriteRenderer playerSpr;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update

  
    public void Start()
    {
        
        health = maxHealth;

    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Player nhận điểm: " + amount + " | Tổng điểm: " + score);
    }

    // Update is called once per frame
    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0)
        {   
            die = true;
            //playerSpr.enabled = false;
            //playerMovement.enabled = false;
        }
    }

}
