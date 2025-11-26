using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public GameObject startPage;
    public GameObject mapSelectPage;  

    void Start()
    {
        startPage.SetActive(true);
        mapSelectPage.SetActive(false);
    }

    public void GoToMapSelect()
    {
        startPage.SetActive(false);
        mapSelectPage.SetActive(true);
    }

    public void GoBackToStart()
    {
        startPage.SetActive(true);
        mapSelectPage.SetActive(false);
    }
}
