using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
        Debug.Log("🎯 LevelCompleteManager STARTED - 1 Enemy Test!");
    }

    void Update()
    {
        int enemyCount = 0;
        
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("EnemySlime") && obj.activeInHierarchy)
            {
                enemyCount++;
                Debug.Log($"🔍 ACTIVE EnemySlime: {obj.name}");
            }
        }

        Debug.Log($"📊 ACTIVE ENEMYSLIMES: {enemyCount}");

        if (enemyCount == 0)
        {
            Debug.Log("🎉 ALL ENEMIES DEAD - SHOWING UI!");
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            return;
        }
    }
}