using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Load a specific level by name
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Restart the current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Go back to the Main Scene (menu)
    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
