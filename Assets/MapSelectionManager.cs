using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionManager : MonoBehaviour
{
    public string[] mapSceneNames; 

    private int selectedMapIndex = 0;

    public void SelectMap(int index)
    {
        selectedMapIndex = index;
        PlayerPrefs.SetInt("SelectedMap", selectedMapIndex);

        // Go to Character Selection Screen
        SceneManager.LoadScene("CharacterSelectScene");
    }
}
