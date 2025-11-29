using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    private SlimeController[] allSlimes;
    private bool uiShown = false;

    void Start()
    {
        // Find ALL SlimeControllers in scene
        allSlimes = FindObjectsOfType<SlimeController>();
        Debug.Log($"🎯 Found {allSlimes.Length} Slimes!");
        
        // Hide UI initially
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (uiShown) return;

        // Count ACTIVE (alive) slimes
        int aliveSlimes = 0;
        foreach (SlimeController slime in allSlimes)
        {
            if (slime != null && !slime.gameObject.activeInHierarchy == false)
            {
                aliveSlimes++;
            }
        }

        // ALL SLIMES DEAD → SHOW UI!
        if (aliveSlimes == 0)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f; // Pause game
            uiShown = true;
            Debug.Log("🎉 LEVEL COMPLETE! All slimes defeated!");
        }
    }
}