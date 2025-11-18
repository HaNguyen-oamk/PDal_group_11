using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 4; // HP
    public float currentHealth;

    public GameObject gameOverUI; // UI panel Game Over
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);

        // sure gameover layer turn off when start
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.SetHealth(currentHealth, maxHealth);
            Die();
            return;
        }

        healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        Debug.Log("Player died!");

        // tắt collider để không nhận thêm damage
        GetComponent<Collider2D>().enabled = false;

        // TODO: play animation die like sprite ***Important
        // GetComponent<Animator>().SetTrigger("die");

        // call UI Game Over
        Invoke("ShowGameOver", 1f);
    }

    void ShowGameOver()
    {
        // turn on screen game over
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        //delete player out scene
        Destroy(gameObject);
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
