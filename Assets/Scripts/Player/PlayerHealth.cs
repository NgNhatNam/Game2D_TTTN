using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{/*
    public float health;
    public float maxHealth;
    private float healthCheckpoint;
    private string healthKey = "PlayerHealth";

    //public int score;
    public int currentScore;          // Điểm hiện tại trong màn chơi
    private int scoreCheckpoint;      // Điểm tại lúc mới vào màn
    private string scoreKey = "PlayerScore";
    
    

    public bool die = false;

    public SpriteRenderer playerSpr;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update

  
    public void Start()
    {
        
        health = maxHealth;

        // Load điểm & máu từ lần qua màn trước (hoặc mặc định)
        currentScore = PlayerPrefs.GetInt(scoreKey, 0);
        health = PlayerPrefs.GetFloat(healthKey, maxHealth);

        // Ghi lại điểm & máu lúc vừa vào màn
        scoreCheckpoint = currentScore;
        healthCheckpoint = health;

    }
    /*
    public void AddScore(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt(scoreKey, score); // Lưu lại điểm
        Debug.Log("Score hiện tại: " + score);
    }
    

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Player nhận điểm: " + amount + " | Tổng điểm: " + currentScore);
    }

    public void ResetToCheckpoint()
    {
        currentScore = scoreCheckpoint;
        maxHealth = healthCheckpoint;
        Debug.Log("Hồi lại điểm và máu khi chết.");
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(scoreKey, currentScore);
        PlayerPrefs.SetFloat(healthKey, maxHealth);
        PlayerPrefs.Save();
        Debug.Log("Đã lưu máu và điểm.");
    }

    
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
    */
    [Header("Máu")]
    public float health;
    public float maxHealth;

    [Header("Điểm")]
    public int currentScore;

    // Dữ liệu tạm tại thời điểm bắt đầu màn (checkpoint)
    private float healthCheckpoint;
    private int scoreCheckpoint;

    // Key dùng để lưu theo toàn game
    private const string MaxHealthKey = "Player_MaxHealth";
    private const string ScoreKey = "PlayerScore";

    public bool die = false;

    void Start()
    {
        // Load dữ liệu tổng dùng để chơi xuyên màn
        currentScore = PlayerPrefs.GetInt(ScoreKey, 0);
        maxHealth = PlayerPrefs.GetFloat(MaxHealthKey, 4f);
        health = maxHealth;

        // Ghi snapshot lại để dùng nếu chết
        scoreCheckpoint = currentScore;
        healthCheckpoint = health;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Player nhận điểm: " + amount + " | Tổng điểm: " + currentScore);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            die = true;
        }
    }

    public void ResetToCheckpoint()
    {
        currentScore = scoreCheckpoint;
        health = healthCheckpoint;
        Debug.Log("Khôi phục lại điểm và máu đầu màn.");
    }

    // Lưu tiến trình khi qua màn → được gọi trong FinishPoint
    public void SaveLevelProgress()
    {
        string levelKey = SceneManager.GetActiveScene().name;

        // Lưu dữ liệu riêng cho màn này
        PlayerPrefs.SetInt(levelKey + "_Score", currentScore);
        PlayerPrefs.SetFloat(levelKey + "_MaxHealth", maxHealth);

        // Cập nhật để dùng tiếp cho màn sau
        PlayerPrefs.SetInt("PlayerScore", currentScore);
        PlayerPrefs.SetFloat("Player_MaxHealth", maxHealth);

        PlayerPrefs.Save();

        Debug.Log($"[SAVE] {levelKey} | Score = {currentScore} | MaxHealth = {maxHealth}");
    }

    // Gọi khi nhặt item tăng máu vĩnh viễn ( Cái này chưa dùng)
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        health = maxHealth;
        PlayerPrefs.SetFloat(MaxHealthKey, maxHealth);
        PlayerPrefs.Save();
        Debug.Log("Tăng máu vĩnh viễn. MaxHealth mới: " + maxHealth);
    }
}
