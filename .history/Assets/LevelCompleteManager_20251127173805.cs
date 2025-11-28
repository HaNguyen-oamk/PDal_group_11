using System.Linq;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI; // Drag your LevelCompleteUI here
    
    private bool uiShown = false; // Prevent showing multiple times

    void Update()
    {
        if (uiShown) return; // Already shown
        
        // Check if ALL EnemySlimes are dead
        EnemySlime[] enemies = FindObjectsOfType<EnemySlime>();
        bool allDead = enemies.Length > 0 && enemies.All(e => !e.gameObject.activeInHierarchy);
        
        // Show UI if all enemies dead
        if (allDead && levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f; // Pause game
            uiShown = true; // Prevent showing again
            Debug.Log("🎉 LEVEL COMPLETE!");
        }
    }
}