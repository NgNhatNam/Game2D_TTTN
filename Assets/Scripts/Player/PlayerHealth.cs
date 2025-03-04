using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public SpriteRenderer playerSpr;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float amount) 
    {
        health -= amount;
        if(health <= 0)
        {
            playerSpr.enabled = false;
            playerMovement.enabled = false;
        }
    }
}
