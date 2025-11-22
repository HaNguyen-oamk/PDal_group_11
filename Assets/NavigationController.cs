using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public GameObject startPage;      // Title Screen Panel
    public GameObject mapSelectPage;  // Map Select Panel

    void Start()
    {
        // Make sure only the title page shows at start
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
