using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPool : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;
        public int spawnCount;
    }

    [SerializeField] private PlayerHealth playerHealth; // Gán trong Inspector
    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private DoorRoom targetDoor;

    private int _totalEnemies;
    private int _defeatedEnemies;

   

    void Start()
    {
        // Tự tìm PlayerHealth trong scene
        playerHealth = FindObjectOfType<PlayerHealth>();
        CalculateTotalEnemies();
        SpawnAllEnemies();
        
        
    }

    private void CalculateTotalEnemies()
    {
        _totalEnemies = 0;
        foreach (EnemyType type in enemyTypes)
        {
            _totalEnemies += type.spawnCount;
        }
    }


    private void SpawnAllEnemies()
    {
        _totalEnemies = 0; // Khởi tạo lại mỗi lần spawn

        foreach (EnemyType type in enemyTypes)
        {
            for (int i = 0; i < type.spawnCount; i++)
            {
                GameObject enemyObj = Instantiate(type.prefab, transform.position, Quaternion.identity);
                if (enemyObj.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.OnDeath += HandleEnemyDeath;

                    if (playerHealth != null)
                    {
                        enemy.OnScoreReward += playerHealth.AddScore;
                    }

                    _totalEnemies++; // 👉 Chỉ tính khi thực sự spawn thành công
                }
            }

        }

        Debug.Log($"Spawned {_totalEnemies} enemies.");
    }

    private void HandleEnemyDeath()
    {
        _defeatedEnemies++;
        Debug.Log($"Enemy chết: {_defeatedEnemies}/{_totalEnemies}");
        CheckAllEnemiesDefeated();
    }

    private void CheckAllEnemiesDefeated()
    {
        if (_defeatedEnemies >= _totalEnemies && targetDoor != null)
        {
            targetDoor.Open();
        }
    }
}