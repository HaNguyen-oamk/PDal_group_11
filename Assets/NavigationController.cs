using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public GameObject startPage;
    public GameObject mapSelectPage;  
    public GameObject characterSelectPage;

    void Start()
    {
        startPage.SetActive(true);
        mapSelectPage.SetActive(false);
        characterSelectPage.SetActive(false);
    }

    public void GoToMapSelect()
    {
        startPage.SetActive(false);
        mapSelectPage.SetActive(true);
        characterSelectPage.SetActive(false);
    }

    public void GoToCharacterSelect()
    {
        startPage.SetActive(false);
        mapSelectPage.SetActive(false);
        characterSelectPage.SetActive(true);
    }

    public void GoBackToStart()
    {
        startPage.SetActive(true);
        mapSelectPage.SetActive(false);
        characterSelectPage.SetActive(false);
    }

    public void GoBackToMapSelect()
    {
        startPage.SetActive(false);
        mapSelectPage.SetActive(true);
        characterSelectPage.SetActive(false);
    }
}
