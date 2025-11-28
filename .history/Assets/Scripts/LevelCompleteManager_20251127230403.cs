using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    private int initialSlimeCount = 0;
    private bool uiShown = false;

    void Start()
    {
        // Count ALL EnemySlimes at start
        initialSlimeCount = CountEnemySlimes();
        Debug.Log($"🎯 Found {initialSlimeCount} EnemySlimes!");
        
        // Hide UI initially
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (uiShown) return;

        // Count CURRENT active EnemySlimes
        int currentSlimes = CountEnemySlimes();
        Debug.Log($"🔍 Active EnemySlimes: {currentSlimes}/{initialSlimeCount}");

        // ALL SLIMES DEAD → SHOW UI!
        if (currentSlimes == 0 && initialSlimeCount > 0)
        {
            ShowLevelComplete();
        }
    }

    int CountEnemySlimes()
    {
        int count = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in allObjects)
        {
            // Count ALL objects with "EnemySlime" in name that are ACTIVE
            if (obj.name.Contains("EnemySlime") && obj.activeInHierarchy)
            {
                count++;
            }
        }
        
        return count;
    }

    void ShowLevelComplete()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause game
        uiShown = true;
        Debug.Log("🎉 LEVEL COMPLETE! All 4 EnemySlimes defeated!");
    }
}