using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] characters;   
    private int selectedCharacterIndex = 0;

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);

        // Load the previously chosen map
        string map = PlayerPrefs.GetString("SelectedMap");
        SceneManager.LoadScene(map);
    }

    public void ConfirmCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);

        string map = PlayerPrefs.GetString("SelectedMap");
        SceneManager.LoadScene(map);
    }
}
