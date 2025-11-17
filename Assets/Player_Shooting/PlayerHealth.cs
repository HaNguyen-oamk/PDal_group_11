using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; //max blood
    private int currentHealth;

    public UnityEngine.UI.Image healthFill;   // <- add blood bar

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
