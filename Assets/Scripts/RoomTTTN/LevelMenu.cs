using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    public Button[] buttons;
    public GameObject levelButtons;

    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++) 
        { 
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++) 
        {
            buttons[i].interactable = true;
        }

    }
    /*
    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }*/

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;

        // Nếu là level đầu tiên, lấy chính nó
        int loadFromLevel = Mathf.Max(1, levelId - 1);
        string loadFromName = "Level " + loadFromLevel;

        // Lấy dữ liệu từ level trước đó
        int score = PlayerPrefs.GetInt(loadFromName + "_Score", 0);
        float maxHealth = PlayerPrefs.GetFloat(loadFromName + "_MaxHealth", 4f);

        // Gán vào PlayerPrefs dùng chung
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetFloat("Player_MaxHealth", maxHealth);
        PlayerPrefs.Save();

        Debug.Log($"[LOAD LEVEL {levelId}] -> Dùng dữ liệu từ {loadFromName} | Score = {score} | MaxHealth = {maxHealth}");

        SceneManager.LoadScene(levelName);
    }
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Đã xóa toàn bộ dữ liệu!");
    }
}
