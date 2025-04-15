using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   
    /*
    public GameObject pauseMenu;
    public static bool isPaused; // Biến kiểm tra trạng thái pause

    public void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void Update()
    {
        // Kiểm tra nếu nhấn phím Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // Nếu đang pause, tiếp tục game
            }
            else
            {
                Pause(); // Nếu không pause, bật pause menu
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true); // Bật Pause Menu
        Time.timeScale = 0f; // Dừng thời gian game
        isPaused = true; // Cập nhật trạng thái pause
    } 

    public void Quit()
    {
        //Time.timeScale = 1; // Đảm bảo thời gian game được khôi phục
        //SceneManager.LoadScene("Main Menu"); // Tải scene Main Menu
    }

    public void Resume()
    {
        Debug.Log("Resume button clicked!");
        pauseMenu.SetActive(false); // Tắt Pause Menu
        Time.timeScale = 1f; // Khôi phục thời gian game
        isPaused = false; // Cập nhật trạng thái không pause
    }

    public void Restart()
    {
        Debug.Log("Restart button clicked!");
        //Time.timeScale = 1; // Đảm bảo thời gian game được khôi phục
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại
    }
    */
    [Header("UI Panel")]
    public GameObject pauseMenu;

    private bool isPaused = false;


    void Update()
    {
        

        // Kiểm tra nếu nhấn phím Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Nếu đang pause, tiếp tục game
            }
            else
            {
                PauseGame(); // Nếu không pause, bật pause menu
            }
        }
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
        
        
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSettings()
    {
        
            pauseMenu.SetActive(false);
       
    }

    public void CloseSettings()
    {
        
            //settingsPanel.SetActive(false);
            pauseMenu.SetActive(true);
        
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Giả sử Level 0 là Main Menu
    }
}
