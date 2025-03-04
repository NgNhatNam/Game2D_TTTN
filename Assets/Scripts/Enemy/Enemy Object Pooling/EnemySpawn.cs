using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Prefabs của các enemy và boss
    public GameObject[] enemyPrefabs; // Mảng chứa các prefab enemy (enemy1, enemy2,...)
    public GameObject bossPrefab;     // Prefab của boss

    // Các thông số spawn
    public int currentLevel = 1;      // Level hiện tại
    public int maxLevel = 5;          // Level tối đa (level cuối cùng sẽ spawn boss)
    public int enemiesPerLevel = 3;   // Số lượng enemy spawn mỗi level
    public Transform[] spawnPoints;   // Các điểm spawn enemy

    // Thời gian giữa các lần spawn
    public float spawnDelay = 1f;

    private int spawnedEnemiesCount = 0; // Đếm số enemy đã spawn trong level hiện tại

    void Start()
    {
        // Bắt đầu spawn enemy cho level đầu tiên
        StartCoroutine(SpawnEnemiesForLevel());
    }

    public void NextLevel()
    {
        // Tăng level và bắt đầu spawn enemy cho level tiếp theo
        currentLevel++;
        spawnedEnemiesCount = 0;
        StartCoroutine(SpawnEnemiesForLevel());
    }

    IEnumerator SpawnEnemiesForLevel()
    {
        if (currentLevel > maxLevel)
        {
            Debug.Log("Game Over! You have reached the final level.");
            yield break;
        }

        // Nếu là level cuối cùng, spawn boss
        if (currentLevel == maxLevel)
        {
            SpawnBoss();
            yield break;
        }

        // Spawn enemy theo level
        for (int i = 0; i < enemiesPerLevel; i++)
        {
            // Chọn ngẫu nhiên một prefab enemy từ mảng
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];

            // Chọn ngẫu nhiên một điểm spawn
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

            // Spawn enemy tại điểm spawn
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);


            // Tăng số lượng enemy đã spawn
            spawnedEnemiesCount++;

            // Đợi trước khi spawn enemy tiếp theo
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnBoss()
    {
        // Chọn ngẫu nhiên một điểm spawn
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        // Spawn boss tại điểm spawn
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

        Debug.Log("Boss has been spawned!");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Gizmos.DrawSphere(spawnPoint.position, 0.5f); // Vẽ một hình cầu nhỏ tại mỗi điểm spawn
            }
        }
    }
}
