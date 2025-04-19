using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject pauseMenu;
    public GameObject settingsPanel;
    public GameObject playerDeathPanel;

    private static bool isPaused = false;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        // Tắt hết các UI ban đầu
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        playerDeathPanel.SetActive(false);
    }

    public void Update()
    {
        // Mở/Tắt Pause Menu khi nhấn ESC và không đang chết
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
        if (playerHealth != null && playerHealth.die)
        {
            ShowDeathScreen();
        }

    }

    public void ShowDeathScreen()
    {
        isPaused = true;
        Time.timeScale = 0f;
        playerDeathPanel.SetActive(true);
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        playerDeathPanel.SetActive(false);
    }

    public void ReloadLevel()
    {
        // Gọi từ UI khi chết hoặc từ menu
        ResumeGame();
        playerHealth.ResetToCheckpoint();
        playerHealth.die = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
