using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    //[SerializeField]
    //bool goNextLevel;
    //[SerializeField]
    //string levelName;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goNextLevel)
            {
                SceneController.instance.NextLevel();
            }
            else
            {
                SceneController.instance.LoadScene(levelName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            SceneController.instance.NextLevel();

        }
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) +1 );
            PlayerPrefs.Save();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.SaveLevelProgress(); // Lưu score + maxHealth
                playerHealth.IncreaseMaxHealth(2);
            }

            UnlockNewLevel();
            SceneController.instance.NextLevel(); // Chuyển màn
        }
    }

    void UnlockNewLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int reached = PlayerPrefs.GetInt("ReachedIndex", 1);

        if (currentIndex >= reached)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }


}
