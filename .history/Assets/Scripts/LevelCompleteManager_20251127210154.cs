using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI;
    private bool uiShown = false;

    void Update()
    {
        if (uiShown) return;

        // 🔥 DEBUG: Count EVERY frame
        int enemyCount = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Enemy") || obj.name.Contains("Slime"))
            {
                enemyCount++;
                Debug.Log($"🔍 ENEMY FOUND: {obj.name} - Active: {obj.activeInHierarchy}");
            }
        }
        
        Debug.Log($"📊 TOTAL ENEMIES NOW: {enemyCount}");
        
        // Trigger when ZERO enemies
        if (enemyCount == 0 && levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
            uiShown = true;
            Debug.Log("🎉 LEVEL COMPLETE TRIGGERED!");
        }
    }
}