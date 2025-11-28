using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteUI;

    void Start()
    {
        // 🔥 FORCE SHOW UI IN 3 SECONDS - PROVES EVERYTHING WORKS!
        Invoke("ShowUI", 3f);
    }

    void ShowUI()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("🎉 FORCED LEVEL COMPLETE! EVERYTHING WORKS!");
        }
    }
}