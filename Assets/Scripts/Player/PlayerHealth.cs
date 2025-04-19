using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
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
    
    private AudioManager audioManager;


    public bool die = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

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
        audioManager.PlaySFX(audioManager.coin);
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

    // Reset lại số điểm
    public void ResetToCheckpoint()
    {
        currentScore = scoreCheckpoint;
        health = healthCheckpoint;
        die = false;
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

    // Gọi khi nhặt item tăng máu vĩnh viễn 
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        health = maxHealth;
        PlayerPrefs.SetFloat(MaxHealthKey, maxHealth);
        PlayerPrefs.Save();
        Debug.Log("Tăng máu. MaxHealth mới: " + maxHealth);
    }
}
