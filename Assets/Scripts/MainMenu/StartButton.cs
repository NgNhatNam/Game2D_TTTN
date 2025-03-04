using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneToLoad;
    

    // Update is called once per frame
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
