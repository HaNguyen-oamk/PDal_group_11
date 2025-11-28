using System.Linq;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI; // Drag your LevelCompleteUI here
    
    void Update()
    {
        // Check if ALL EnemySlimes are dead
        EnemySlime[] enemies = FindObjectsOfType<EnemySlime>();
        bool allDead = enemies.Length > 0 && enemies.All(e => !e.gameObject.activeInHierarchy);
        
        // Show UI if all enemies dead
        if (allDead && levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }
    }
}