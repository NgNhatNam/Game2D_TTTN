using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Tham chiếu đến SpawnEnemy để quản lý việc spawn enemy/boss
    public EnemySpawn spawnEnemy;

    // Số lượng enemy còn lại trong scene
    private int remainingEnemies = 0;

    void Start()
    {
        // Bắt đầu với level đầu tiên
        UpdateRemainingEnemies();
    }

    void Update()
    {
        // Kiểm tra số lượng enemy còn lại
        CheckEnemies();
    }

    // Cập nhật số lượng enemy còn lại trong scene
    public void UpdateRemainingEnemies()
    {
        remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log($"Remaining enemies: {remainingEnemies}");
    }

    // Kiểm tra số lượng enemy còn lại
    void CheckEnemies()
    {
        // Nếu không còn enemy nào, chuyển sang level tiếp theo
        if (remainingEnemies <= 0)
        {
            Debug.Log("All enemies defeated! Moving to the next level...");
            spawnEnemy.NextLevel();
            UpdateRemainingEnemies(); // Cập nhật lại số lượng enemy cho level mới
        }
    }

    // Phương thức để giảm số lượng enemy khi một enemy bị tiêu diệt
    public void EnemyDefeated()
    {
        remainingEnemies--;
        Debug.Log($"An enemy has been defeated. Remaining enemies: {remainingEnemies}");
    }
}
