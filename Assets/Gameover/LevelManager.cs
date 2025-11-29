using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // --- Public Settings ---
    public int level = 1;

    // Drag SpawnPoints here
    public Transform[] spawnPoints; 
    public GameObject slimePrefab;
    public GameObject skeletonPrefab;

    // UI Panel that pops up when the level is finished
    public GameObject levelCompleteUI; 

    // --- Private State ---
    private int enemiesAlive = 0;

    void Start()
    {
        // Ensure the game starts running normally
        Time.timeScale = 1f;
        
        // Hide the Level Complete UI initially
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
            
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        int slimeCount = level * 2;
        int skeletonCount = level;

        enemiesAlive = slimeCount + skeletonCount;

        // Spawn Slimes
        for (int i = 0; i < slimeCount; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(slimePrefab, spawnPoints[index].position, Quaternion.identity);
        }

        // Spawn Skeletons
        for (int i = 0; i < skeletonCount; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(skeletonPrefab, spawnPoints[index].position, Quaternion.identity);
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            // Show the UI when all enemies are defeated
            levelCompleteUI.SetActive(true);
            // Optional: Pause the game when level is complete if needed
            // Time.timeScale = 0f; 
        }
    }

    // --- Button Functions ---

    // Function called by the "Next Level" Button
    public void NextLevel()
    {
        // Ensure the game is running normally before spawning new enemies
        Time.timeScale = 1f; 
        
        level++;
        levelCompleteUI.SetActive(false);
        SpawnEnemies();
    }

    // Function called by the "Restart" Button (e.g., from Game Over or Pause Menu)
    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function called by the "Exit" Button
    public void ExitToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}