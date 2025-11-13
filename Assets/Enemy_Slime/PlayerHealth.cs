using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1; //can change
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            GetComponent<SlimeController>()?.TakeDamage(); // call animation die
        }
    }
}
