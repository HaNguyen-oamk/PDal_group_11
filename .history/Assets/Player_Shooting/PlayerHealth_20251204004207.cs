using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 4; 
    public float currentHealth;

    public GameObject gameOverUI; 
    public HealthBar healthBar;    

    Animator anim;
    Collider2D col;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        // ---- AUTO FIND HEALTH BAR ----
        if (healthBar == null)
        {
            GameObject hb = GameObject.Find("HealthBarBG"); 
            if (hb != null)
                healthBar = hb.GetComponent<HealthBar>();
            else
                Debug.LogWarning("do not find HealthBarBG in scene!");
        }

        // ---- AUTO FIND GAME OVER UI ----
        if (gameOverUI == null)
        {
            gameOverUI = GameObject.Find("GameOverUI");
            if (gameOverUI == null)
                Debug.LogWarning("do not find GameOverUI in scene!");
        }

        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        // hide GameOver 
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
{
    currentHealth = Mathf.Min(currentHealth + amount, maxHealth);  // Can't go over 4
    if (healthBar != null)
        healthBar.SetHealth(currentHealth, maxHealth);
    Debug.Log("Healed! Health: " + currentHealth + "/" + maxHealth);
}

    void Die()
    {
        Debug.Log("Player died!");
        currentHealth = 0;

        col.enabled = false;

        if (anim != null)
            anim.SetBool("isDead", true);

        Invoke(nameof(ShowGameOver), 1f);
    }

    void ShowGameOver()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Destroy(gameObject);
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
