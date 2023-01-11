using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationLogic : MonoBehaviour
{
    public void EnterButton(int value)
    {
        if (value == 0)
            SceneManager.LoadScene("MapScene");
        else if (value == 1)
            SceneManager.LoadScene("SampleScene");
        else if (value == 2)
            SceneManager.LoadScene("CityScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
