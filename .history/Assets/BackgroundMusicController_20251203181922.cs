using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {
    private AudioSource musicSource;
    public GameObject gameOverUI;  // Drag GameOverUI here (one-time setup)
    
    void Start() {
        musicSource = GetComponent<AudioSource>();  // Your syl_mp3 loop
    }
    
    void Update() {
        // Check if GameOverUI just popped up → STOP music instantly!
        if (gameOverUI != null && gameOverUI.activeInHierarchy && musicSource.isPlaying) {
            musicSource.Stop();  // Silence! 🎵➡️🔇
        }
    }
}
