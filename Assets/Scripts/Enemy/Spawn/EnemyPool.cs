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
        SpawnAllEnemies();
        CalculateTotalEnemies();
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
        foreach (EnemyType type in enemyTypes)
        {
            for (int i = 0; i < type.spawnCount; i++)
            {
                GameObject enemyObj = Instantiate(type.prefab, transform.position, Quaternion.identity);
                if (enemyObj.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.OnDeath += HandleEnemyDeath;

                    // Gán sự kiện cộng điểm cho Player
                    if (playerHealth != null)
                    {
                        enemy.OnScoreReward += playerHealth.AddScore;
                    }
                }
            }
        }
    }

    private void HandleEnemyDeath()
    {
        _defeatedEnemies++;
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