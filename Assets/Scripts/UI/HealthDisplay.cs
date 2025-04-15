using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    public TMP_Text scoreText;
    public float health;
    public float playerMaxHealth;
    public PlayerHealth playerHealth;


    void Update()
    {
        // Đưa điểm lên màn hình 
        if (playerHealth != null && scoreText != null)
        {
            scoreText.text = "Score: " + playerHealth.currentScore.ToString();
        }
        //Đưa thanh máu lên màn hình
        health = playerHealth.health;
        playerMaxHealth = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.FloorToInt(health/2)) // Số lượng trái tim đầy
            {
                hearts[i].sprite = fullHeart; // Đầy trái tim
            }
            else if (i == Mathf.FloorToInt(health / 2) && health % 2 != 0) // Kiểm tra nửa trái tim
            {
                hearts[i].sprite = halfHeart; // Nửa trái tim
            }
            else if (i < Mathf.CeilToInt(playerMaxHealth/2)) // Trái tim trống nhưng vẫn hiển thị
            {
                hearts[i].sprite = emptyHeart; 
            }
            else
            {
                hearts[i].enabled = false; // Tắt trái tim nếu vượt quá maxHealth
                continue;
            }

            // Kích hoạt trái tim nếu nó nằm trong phạm vi maxHealth
            hearts[i].enabled = true;
        }

    
    }
}
