using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sc

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuPanel;


    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
}
