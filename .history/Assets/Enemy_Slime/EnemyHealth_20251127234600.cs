using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1; 
    public float currentHealth;

    [Header("UI")]
    public GameObject levelCompleteUI; 

    Animator anim;
    Collider2D col;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        // ---- AUTO FIND LEVEL COMPLETE UI ----
        if (levelCompleteUI == null)
        {
            levelCompleteUI = GameObject.Find("LevelCompleteUI");
            if (levelCompleteUI == null)
                Debug.LogWarning("⚠️ LevelCompleteUI not found in scene!");
        }

        currentHealth = maxHealth;

        // Hide LevelCompleteUI initially
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"💥 Enemy {gameObject.name} hit! Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"💀 Enemy {gameObject.name} DIED!");

        // Disable collider
        if (col != null)
            col.enabled = false;

        // Play death animation
        if (anim != null)
            anim.SetBool("isDead", true);

        // Check if ALL enemies dead
        CheckVictoryAndShowUI();

        // Destroy enemy after 1 second (like SlimeController)
        Invoke(nameof(DestroyEnemy), 1f);
    }

    void CheckVictoryAndShowUI()
    {
        // Count ALL alive enemies
        int aliveEnemies = 0;
        EnemyHealth[] allEnemies = FindObjectsOfType<EnemyHealth>();
        
        foreach (EnemyHealth enemy in allEnemies)
        {
            if (enemy != null && enemy.currentHealth > 0 && enemy.gameObject.activeInHierarchy)
            {
                aliveEnemies++;
            }
        }

        Debug.Log($"🎯 Enemies alive: {aliveEnemies}");

        // LAST ENEMY = VICTORY!
        if (aliveEnemies == 0)
        {
            Debug.Log("🏆 ALL ENEMIES DEFEATED!");
            Invoke(nameof(ShowLevelComplete), 1f); // 1 second delay
        }
    }

    void ShowLevelComplete()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f; // Pause game
            Debug.Log("🎉 LEVEL COMPLETE UI SHOWN!");
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}