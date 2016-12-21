using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void start(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void EndGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
