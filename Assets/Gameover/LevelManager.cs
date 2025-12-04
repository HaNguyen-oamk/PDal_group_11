using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int level = 1;

    public Transform[] spawnPoints; 
    public GameObject slimePrefab;
    public GameObject skeletonPrefab;

    public GameObject levelCompleteUI; 

    public int totalWaves = 3;  
    private int currentWave = 0;

    private int enemiesAlive = 0;
    public float spawnDelay = 1.5f;

    void Start()
    {
        Time.timeScale = 1f;

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);

        StartNextWave();
    }

    void StartNextWave()
    {
        currentWave++;
        Debug.Log($"Starting wave {currentWave} of {totalWaves}");

        if (currentWave > totalWaves)
        {
            LevelCompleted();
            return;
        }

        StartCoroutine(SpawnWaveEnemies());
    }

    
    IEnumerator SpawnWaveEnemies()
    {
        int slimeCount = level * 2 * currentWave;
        int skeletonCount = level * currentWave;

        enemiesAlive = slimeCount + skeletonCount;

        for (int i = 0; i < slimeCount; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(slimePrefab, spawnPoints[index].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay); 
        }

        for (int i = 0; i < skeletonCount; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(skeletonPrefab, spawnPoints[index].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay); 
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        Debug.Log("Enemy died! Remaining: " + enemiesAlive);

        if (enemiesAlive < 0) enemiesAlive = 0;

        if (enemiesAlive <= 0)
        {
            Invoke(nameof(StartNextWave), 1f);
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        level++;
        currentWave = 0; // RESET waves
        levelCompleteUI.SetActive(false);

        StartNextWave();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void LevelCompleted()
    {
        levelCompleteUI.SetActive(true);
    }
}
