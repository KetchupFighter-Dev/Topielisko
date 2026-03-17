using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int baseDamage = 1;  // Podstawowe obra¿enia dla nieodkrytej broni
    public int weaponDamage = 10; // Prawdziwe obra¿enia po odkryciu
    public float attackSpeed = 1f;

    protected bool canAttack = true;
    public bool found = false; // Czy broñ jest odkryta?

    public virtual int GetDamage()
    {
        return found ? weaponDamage : baseDamage;
    }

    public virtual void Attack()
    {
        Debug.Log($"{weaponName} attacked with {GetDamage()} damage!");
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    protected void PerformAttack()
    {
        if (canAttack)
        {
            Debug.Log($"{weaponName} attacked with {weaponDamage} damage!");
            // Tutaj mo¿esz dodaæ logikê animacji ataku
            canAttack = false;
            Invoke("ResetAttack", attackSpeed); // Umo¿liwia atak po up³ywie czasu
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}