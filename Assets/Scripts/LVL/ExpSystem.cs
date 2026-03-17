using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    public int currentExp = 0;

    public void AddExp(int amount)
    {
        currentExp += amount;
    }

    public bool SpendExp(int cost)
    {
        if (currentExp >= cost)
        {
            currentExp -= cost;
            return true;
        }

        return false;
    }
}