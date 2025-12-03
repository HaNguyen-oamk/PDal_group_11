using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public float healAmount = 1f;  // +1 health

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.Heal(healAmount);  // Calls your Heal()!
                
                // NEW: Tell spawner this heart was collected
                SpawnHearts spawner = FindObjectOfType<SpawnHearts>();
                if (spawner != null)
                    spawner.HeartCollected();
                
                Destroy(gameObject);      // Heart vanishes
            }
        }
    }
}
    

    public void HeartCollected()
    {
        currentHearts = Mathf.Max(0, currentHearts - 1);
    }
