using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenue : MonoBehaviour
{
    public GameObject Menuobject;
    void Start()
    {
        Menuobject.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Menuobject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Menuobject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void EndGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ResumeGame()
    {
        Menuobject.SetActive(false);
        Time.timeScale = 1;
    }
}
