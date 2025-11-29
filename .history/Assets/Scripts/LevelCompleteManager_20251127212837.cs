using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("🎉 LEVEL COMPLETE SCRIPT STARTED - PERFECT!");
    }

    void Update()
    {
        // Press SPACE to show UI INSTANTLY
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("🚀 SPACE PRESSED - SHOWING UI!");
            
            GameObject ui = GameObject.Find("LevelCompleteUI");
            if (ui != null)
            {
                ui.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("🎉 VICTORY UI SHOWED!");
            }
            else
            {
                Debug.LogError("❌ LevelCompleteUI NOT FOUND!");
            }
        }
    }
}