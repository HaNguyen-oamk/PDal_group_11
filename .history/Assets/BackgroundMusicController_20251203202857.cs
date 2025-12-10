using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [Header("Drag your GameOverUI here")]
    public GameObject gameOverUI;
    
    private AudioSource musicSource;
    private bool gameOverTriggered = false;  // Prevent multiple stops
    
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        
        // Hide GameOverUI at start (if not already)
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }
    
    void Update()
    {
        // When GameOverUI pops up (player killed) → STOP MUSIC INSTANTLY
        if (gameOverUI != null && 
            gameOverUI.activeInHierarchy && 
            !gameOverTriggered && 
            musicSource.isPlaying)
        {
            musicSource.Stop();  // 🔇 Silence!
            gameOverTriggered = true;  // Only stop once
            Debug.Log("Background Music Stopped - Game Over!");
        }
    }
}
