using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void MainmenuBTN()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Scene Loaded");
    }
}
