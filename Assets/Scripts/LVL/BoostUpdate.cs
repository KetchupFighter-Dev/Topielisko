using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class BoostUpgrade : MonoBehaviour
{
    public enum BoostType
    {
        Speed,
        Damage,
        Dash,
    }

    public BoostType boostType;

    [Header("Upgrade")]
    public float boostPerLevel = 0.2f;
    public int baseCost = 50;

    int level = 0;

    playerMovement player;
    ExpSystem exp;

    Button button;

    [Header("UI")]
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;

    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        exp = FindObjectOfType<ExpSystem>();
        button = GetComponent<Button>();

        UpdateUI();
    }

    void Update()
    {
        button.interactable = exp.currentExp >= GetCost();
    }

    public void BuyUpgrade()
    {
        int cost = GetCost();

        if (!exp.SpendExp(cost))
            return;

        level++;

        ApplyBoost();

        UpdateUI();
    }

    int GetCost()
    {
        return baseCost + (level * baseCost);
    }

    void ApplyBoost()
    {
        switch (boostType)
        {
            case BoostType.Speed:
                player.speedBoost += boostPerLevel;
                break;

            case BoostType.Damage:
                player.jumpBoost += boostPerLevel;
                break;

            case BoostType.Dash:
                player.dashBoost += boostPerLevel;
                break;
        }
    }

    void UpdateUI()
    {

        if (costText != null)
            costText.text = boostType.ToString()+"\n"+"Lv. " + level + "\nCost: " + GetCost();
    }
}