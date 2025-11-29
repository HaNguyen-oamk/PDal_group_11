using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI;
    private bool uiShown = false;

    void Start()
    {
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    void Update()
    {
        if (uiShown) return;

        int activeEnemies = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Enemy") || obj.name.Contains("Slime"))
            {
                // 🔥 FIXED: Check if object is ACTIVE (not destroyed)
                if (obj.activeInHierarchy)
                    activeEnemies++;
            }
        }

        // Show UI when ZERO active enemies
        if (activeEnemies == 0 && levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
            uiShown = true;
            Debug.Log("🎉 LEVEL COMPLETE! (0 active enemies)");
        }
    }
}