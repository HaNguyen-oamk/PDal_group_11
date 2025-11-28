using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI;

    void Start()
    {
        Debug.Log("🚀 LEVEL COMPLETE SCRIPT STARTED!");
        
        if (levelCompleteUI == null)
        {
            Debug.LogError("❌ LEVEL COMPLETE UI IS NULL!");
        }
        else
        {
            Debug.Log("✅ LEVEL COMPLETE UI ASSIGNED!");
            levelCompleteUI.SetActive(false);
        }
    }

    void Update()
    {
        Debug.Log("🔥 SCRIPT UPDATE RUNNING EVERY FRAME!");
        
        if (Input.GetKeyDown(KeyCode.V)) // Press V to test
        {
            if (levelCompleteUI != null)
            {
                levelCompleteUI.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("🎉 MANUAL VICTORY TRIGGERED!");
            }
        }
    }
}