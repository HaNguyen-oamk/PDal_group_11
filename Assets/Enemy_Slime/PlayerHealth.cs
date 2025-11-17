using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;   // Số máu tối đa
    private int currentHealth;

    public UnityEngine.UI.Image healthFill;   // <- thêm cái này để fill thanh máu

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player HP: " + currentHealth);

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
    }
}
