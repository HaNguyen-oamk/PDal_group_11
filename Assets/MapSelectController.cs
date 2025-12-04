using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectController : MonoBehaviour
{
    public void OnSelect1()
    {
        SceneManager.LoadScene("Voicecontrol");
    }

    
    public void OnSelect2()
    {
        SceneManager.LoadScene("Voicecontrol 2");
    }

    public void OnSelect3()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnSelectBack()
    {
        SceneManager.LoadScene("StartScene");
    }
}
