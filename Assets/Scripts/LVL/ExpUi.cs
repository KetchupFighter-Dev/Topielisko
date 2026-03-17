using TMPro;
using UnityEngine;

public class ExpUI : MonoBehaviour
{
    public ExpSystem expSystem;
    public TextMeshProUGUI expText;

    void Update()
    {
        expText.text = "EXP: " + expSystem.currentExp;
    }
}