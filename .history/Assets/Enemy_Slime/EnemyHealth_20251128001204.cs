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

        // Auto find UI
        if (levelCompleteUI == null)
        {
            levelCompleteUI = GameObject.Find("LevelCompleteUI");
        }

        currentHealth = maxHealth;

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"Enemy {gameObject.name} hit! Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"Enemy {gameObject.name} DIED!");

        if (col != null)
            col.enabled = false;

        if (anim != null)
            anim.SetBool("isDead", true);

        // After animation, destroy + victory check
        Invoke(nameof(HandleDeathComplete), 1f);
    }

    void HandleDeathComplete()
    {
        Destroy(gameObject);
        CheckVictoryAndShowUI();  // <-- NOW runs AFTER the enemy dies
    }

    void CheckVictoryAndShowUI()
    {
        EnemyHealth[] allEnemies = FindObjectsOfType<EnemyHealth>();
        int aliveEnemies = 0;

        foreach (EnemyHealth e in allEnemies)
        {
            if (e != null && e.currentHealth > 0)
                aliveEnemies++;
        }

        Debug.Log($"Enemies alive: {aliveEnemies}");

        if (aliveEnemies == 0)
        {
            Invoke(nameof(ShowLevelComplete), 0.5f);
        }
    }

    void ShowLevelComplete()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("LEVEL COMPLETE!");
        }
    }
}
