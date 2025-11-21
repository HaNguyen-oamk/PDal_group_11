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
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);

        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        currentHealth = 0;

        col.enabled = false;

        // Animation die
        if (anim != null)
            anim.SetBool("isDead", true);

        // Hiện UI Game Over sau 1s
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
