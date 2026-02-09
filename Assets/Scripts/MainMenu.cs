using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Scene Loaded");
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Scene Loaded");
    }

    public void ExitBtn()
    {
        Application.Quit(69);
    }
}
