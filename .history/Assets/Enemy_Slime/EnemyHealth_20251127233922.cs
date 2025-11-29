using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 1; 
    public float currentHealth;

    public GameObject levelCompleteUI; 

    Animator anim;
    Collider2D col;
    SlimeController slimeController;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        slimeController = GetComponent<SlimeController>();

        // ---- AUTO FIND LEVEL COMPLETE UI ----
        if (levelCompleteUI == null)
        {
            levelCompleteUI = GameObject.Find("LevelCompleteUI");
            if (levelCompleteUI == null)
                Debug.LogWarning("LevelCompleteUI not found in scene!");
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
        Debug.Log($"Enemy {gameObject.name} took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"💀 Enemy {gameObject.name} died!");

        // Disable collider
        if (col != null)
            col.enabled = false;

        // Play death animation
        if (anim != null)
            anim.SetBool("isDead", true);

        // Stop slime movement/attacks
        if (slimeController != null)
        {
            slimeController.isDead = true;
            slimeController.StopAllCoroutines();
        }

        // Check if LAST ENEMY → Show Level Complete
        CheckForVictory();

        // Destroy enemy after animation
        Invoke(nameof(DestroyEnemy), 1f);
    }

    void CheckForVictory()
    {
        // Count remaining enemies
        int aliveEnemies = 0;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in allEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null && enemyHealth.currentHealth > 0 && enemy.activeInHierarchy)
            {
                aliveEnemies++;
            }
        }

        Debug.Log($"🎯 Alive enemies left: {aliveEnemies}");

        // LAST ENEMY DIED = VICTORY!
        if (aliveEnemies == 0)
        {
            Debug.Log("🎉 ALL ENEMIES DEFEATED - LEVEL COMPLETE!");
            Invoke(nameof(ShowLevelComplete), 1f);
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