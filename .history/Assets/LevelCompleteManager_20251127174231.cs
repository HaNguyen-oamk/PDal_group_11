using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI Setup")]
    public GameObject levelCompleteUI; // Drag your LevelCompleteUI Panel here
    
    private bool uiShown = false;

    void Start()
    {
        // Hide UI initially
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    void Update()
    {
        // Prevent multiple triggers
        if (uiShown) return;

        // Count active enemies (ANY with "Enemy" or "Slime" in name)
        int activeEnemies = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Enemy") || obj.name.Contains("Slime"))
            {
                if (obj.activeInHierarchy)
                    activeEnemies++;
            }
        }

        // 🔥 VICTORY: No active enemies left!
        if (activeEnemies == 0 && levelCompleteUI != null)
        {
            ShowLevelCompleteUI();
        }
    }

    private void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
        uiShown = true;
        Debug.Log("🎉 LEVEL COMPLETE! 🦈");
    }
}