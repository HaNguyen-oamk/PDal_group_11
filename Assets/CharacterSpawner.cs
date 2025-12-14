using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public GameObject[] sceneCharacters; 
    public CinemachineVirtualCamera virtualCam;

    void Start()
    {
        // Hide all scene characters
        foreach (var character in sceneCharacters)
        {
            if (character != null)
                character.SetActive(false);
        }

        // Spawn the selected character
        int index = PlayerPrefs.GetInt("SelectedCharacter", 0);

        if (characterPrefabs.Length > index && characterPrefabs[index] != null)
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            GameObject clone = Instantiate(characterPrefabs[index], spawnPos, Quaternion.identity);

            clone.SetActive(true); 

            
            if (virtualCam != null)
                virtualCam.Follow = clone.transform;
        }

        Debug.Log("SelectedCharacter = " + PlayerPrefs.GetInt("SelectedCharacter"));
    }
}
