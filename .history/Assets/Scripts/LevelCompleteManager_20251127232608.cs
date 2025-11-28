using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI; 
    private SlimeController[] allSlimes;
    private bool victoryShown = false;

    void Start()
    {
        // AUTO FIND LevelCompleteUI
        if (levelCompleteUI == null)
        {
            levelCompleteUI = GameObject.Find("LevelCompleteUI");
            if (levelCompleteUI == null)
                Debug.LogWarning("LevelCompleteUI not found in scene!");
        }

        // Find ALL Slimes
        allSlimes = FindObjectsOfType<SlimeController>();
        Debug.Log($"🎯 Found {allSlimes.Length} Slimes!");

        // Hide UI initially
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    void Update()
    {
        if (victoryShown) return;

        // Count ALIVE slimes (same as PlayerHealth Die() logic)
        int aliveSlimes = 0;
        foreach (SlimeController slime in allSlimes)
        {
            if (slime != null && slime.gameObject.activeInHierarchy)
            {
                aliveSlimes++;
            }
        }

        // ALL SLIMES DEAD = VICTORY!
        if (aliveSlimes == 0)
        {
            Die(); // Like PlayerHealth Die()
        }
    }

    void Die()
    {
        Debug.Log("🎉 All Slimes died - Victory!");
        victoryShown = true;

        if (levelCompleteUI != null)
        {
            Invoke(nameof(ShowLevelComplete), 1f); // 1s delay like player
        }
    }

    void ShowLevelComplete()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }
    }
}