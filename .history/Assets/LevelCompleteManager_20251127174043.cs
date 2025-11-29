using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI; // Drag your LevelCompleteUI here
    
    private bool uiShown = false;

    void Update()
    {
        if (uiShown) return;

        // 🔥 UNIVERSAL ENEMY DETECTION - Works with ANY enemy!
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int enemyCount = 0;
        int activeEnemies = 0;

        // Count ALL objects with "Enemy" in name (works with EnemySlime, EnemyShark, etc.)
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Enemy") || obj.name.Contains("Slime"))
            {
                enemyCount++;
                if (obj.activeInHierarchy)
                    activeEnemies++;
            }
        }

        // Show UI if NO active enemies found
        bool allEnemiesDead = enemyCount > 0 && activeEnemies == 0;
        
        if (allEnemiesDead && levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f; // Pause game
            uiShown = true;
            Debug.Log("🎉 LEVEL COMPLETE! All enemies defeated!");
        }
    }
}