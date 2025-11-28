using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject levelCompletePanel;
    
    [Header("Enemy Tracking")]
    [SerializeField] private EnemySlime[] enemies; // Drag all EnemySlimes OR auto-find
    
    [Header("Debug")]
    [SerializeField] private bool autoFindEnemies = true;

    private bool gameActive = true;
    private List<EnemySlime> enemyList = new List<EnemySlime>();

    void Start()
    {
        // Auto-find all EnemySlimes
        if (autoFindEnemies || enemies.Length == 0)
        {
            enemyList = FindObjectsOfType<EnemySlime>().ToList();
            Debug.Log($"🐙 LevelCompleteManager: Found {enemyList.Count} EnemySlimes!");
        }
        else
        {
            enemyList = enemies.ToList();
        }
        
        // Hide UI initially
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    void Update()
    {
        if (!gameActive) return;
        
        // 🔥 WIN CONDITION: ALL EnemySlimes destroyed
        bool allEnemiesDead = enemyList.Count > 0 && 
                             enemyList.All(e => !e.gameObject.activeInHierarchy);
        
        if (allEnemiesDead)
        {
            gameActive = false;
            ShowLevelComplete();
        }
    }

    // 🔥 MAIN FUNCTION: Show Level Complete UI
    public void ShowLevelComplete()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            Time.timeScale = 0f; // PAUSE GAME
            Debug.Log("🎉 LEVEL COMPLETE! All enemies defeated! 🦈");
        }
    }

    // BUTTON FUNCTIONS (for later wiring)
    public void NextLevel()
    {
        Time.timeScale = 1f;
        Debug.Log("Next Level Clicked!");
        // SceneManager.LoadScene("Level2"); // Add later
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Main Menu Clicked!");
        // SceneManager.LoadScene("MainMenu"); // Add later
    }

    // Hide UI (if needed)
    public void HideLevelComplete()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}