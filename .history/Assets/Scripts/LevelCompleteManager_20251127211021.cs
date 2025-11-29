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

        // 🔥 TEST VERSION: Show UI after 10 seconds (for testing)
        if (Time.time > 10f)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
            uiShown = true;
            Debug.Log("🎉 TEST LEVEL COMPLETE!");
            return;
        }

        // 🔥 REAL VERSION: Count enemies
        int enemyCount = 0;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Enemy") || obj.name.Contains("Slime"))
            {
                enemyCount++;
            }
        }

        Debug.Log($"👹 Enemies left: {enemyCount}");

        if (enemyCount == 0)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
            uiShown = true;
            Debug.Log("🎉 REAL LEVEL COMPLETE!");
        }
    }
}