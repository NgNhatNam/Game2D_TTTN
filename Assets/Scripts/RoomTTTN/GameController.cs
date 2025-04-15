using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{/*
    Vector2 startPos;
    //PlayerPrefs playerPr;
    PlayerHealth playerHealth;
    //CameraMovement cam;

    private void Awake()
    {
        //playerPr = GetComponent<PlayerPrefs>();
        playerHealth = GetComponent<PlayerHealth>();
        //cam = Camera.main.GetComponent<CameraMovement>();

    }
    private void Start()
    {
        startPos = transform.position;
        string scoreKey = SceneManager.GetActiveScene().name + "_Score";
        PlayerPrefs.SetInt(scoreKey, 0);

    }
    private void Update()
    {
        if (playerHealth.die == true)
        {
            Die();
        }
    }


    void Die()
    {
        playerHealth.ResetToCheckpoint(); // 👉 Khôi phục máu và điểm
        StartCoroutine(Respawn(0.07f));

    }
    IEnumerator Respawn(float duration)
    {
        
        //PlayerPrefs.DeleteAll();
        transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(duration);
        playerHealth.die = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //playerHealth.die = false;
        //playerHealth.health = playerHealth.maxHealth;
        //transform.position = startPos;
        //transform.localScale = new Vector3(1,1,1);

    }*/

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth != null && playerHealth.die)
        {
            Die();
        }
    }

    void Die()
    {
        //PlayerPrefs.DeleteAll();
        playerHealth.ResetToCheckpoint(); // hồi điểm + máu
        StartCoroutine(Respawn(0.07f));
    }

    IEnumerator Respawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
