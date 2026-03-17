using UnityEngine;

public class ToggleBoostUI : MonoBehaviour
{
    public GameObject boostCanvas;

    bool isOpen = false;

    public void ToggleUI()
    {
        isOpen = !isOpen;

        boostCanvas.SetActive(isOpen);
    }
}