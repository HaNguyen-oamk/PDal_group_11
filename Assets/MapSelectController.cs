using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectController : MonoBehaviour
{
    public NavigationController navigation;

    public void OnSelect1()
    {
        PlayerPrefs.SetString("SelectedMap", "Voicecontrol");
        navigation.GoToCharacterSelect();
    }

    public void OnSelect2()
    {
        PlayerPrefs.SetString("SelectedMap", "Voicecontrol 2");
        navigation.GoToCharacterSelect();
    }

    public void OnSelect3()
    {
        PlayerPrefs.SetString("SelectedMap", "SampleScene");
        navigation.GoToCharacterSelect();
    }

    public void OnSelectBack()
    {
        navigation.GoBackToStart();
    }
}
