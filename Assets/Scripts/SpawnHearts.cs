using UnityEngine;

public class SpawnHearts : MonoBehaviour
{
    [Header("Heart Settings")]
    public GameObject heartPrefab;      // Drag Heart prefab
    public float spawnDelay = 3f;       // Wait 3 sec between spawns
    public int maxHeartsOnScreen = 2;   // Max 2 hearts at once
    public Vector2 spawnAreaMin = new Vector2(-7, -4);  // Your grass bounds
    public Vector2 spawnAreaMax = new Vector2(7, 4);
    
    [Header("Player Health Check")]
    public PlayerHealth playerHealth;   // Drag Shark-player here
    
    private float spawnTimer = 0f;
    private int currentHearts = 0;

    void Start()
    {
        // Auto-find player if not assigned
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        // Only spawn if health < 2 AND less than max hearts on screen
        if (playerHealth != null && 
            playerHealth.currentHealth < 3f && 
            currentHearts < maxHeartsOnScreen)
        {
            spawnTimer += Time.deltaTime;
            
            if (spawnTimer >= spawnDelay)
            {
                SpawnSingleHeart();
                spawnTimer = 0f;  // Reset timer
            }
        }
    }

    void SpawnSingleHeart()
    {
        Vector2 pos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        
        GameObject heart = Instantiate(heartPrefab, pos, Quaternion.identity);
        currentHearts++;

        // Listen for heart collection
        HeartPickup pickup = heart.GetComponent<HeartPickup>();
        if (pickup != null)
        {
            // Simple way: Check every frame (efficient enough for 2 hearts)
        }
    }

    // Call this when heart is collected (add to HeartPickup script)
    public void HeartCollected()
    {
        currentHearts--;
        Debug.Log("Heart collected! Active hearts: " + currentHearts);
    }
}
